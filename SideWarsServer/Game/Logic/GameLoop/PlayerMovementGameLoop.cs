using SideWars.Shared.Packets;
using SideWars.Shared.Physics;
using SideWarsServer.Game.Logic.Effects;
using SideWarsServer.Game.Room;
using SideWarsServer.Networking;
using SideWarsServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideWarsServer.Game.Logic.GameLoop
{
    public class PlayerMovementGameLoop : IGameLoop
    {
        class Buffer
        {
            public PlayerConnection PlayerConnection { get; set; }
            public float Horizontal { get; set; }
            public PlayerButton[] Buttons { get; set; }
        }

        private Queue<Buffer> movementBuffer;

        public PlayerMovementGameLoop()
        {
            movementBuffer = new Queue<Buffer>();
        }

        public void Update(IGameRoom gameRoom)
        {
            while (movementBuffer.Count > 0)
            {
                var buffer = movementBuffer.Dequeue();
                var player = gameRoom.GetPlayer(buffer.PlayerConnection.Token.ID);

                var playerMovement = (PlayerMovement)player.Movement;
                playerMovement.Horizontal = buffer.Horizontal;

                // Update movement if it's not halted
                if (!playerMovement.IsHalted)
                {
                    var location = player.Location;
                    playerMovement.Update(LogicTimer.FixedDelta, ref location);
                    player.Location = location;
                }

                foreach (var button in buffer.Buttons)
                {
                    if (button == PlayerButton.Special1 || button == PlayerButton.Special2)
                    {
                        var spell = player.PlayerSpells.SpellInfo.GetSpellInfo(button);

                        if (player.PlayerSpells.Cast(gameRoom, player, spell))
                        {
                            gameRoom.GetGameLoop<PacketSenderGameLoop>().OnSpellUse(spell, player);
                        }
                    }
                    else if (button == PlayerButton.Fire)
                    {
                        // TODO: Change this
                        if (player.PlayerCombat.Shoot())
                        {
                            new PlayerShootEffect(player).Start(gameRoom);
                        }
                    }
                }
            }
        }

        public void AddBuffer(PlayerConnection connection, float horizontal, PlayerButton[] buttons)
        {
            lock (movementBuffer)
                movementBuffer.Enqueue(new Buffer() { PlayerConnection = connection, Horizontal = horizontal, Buttons = buttons });
        }
    }
}
