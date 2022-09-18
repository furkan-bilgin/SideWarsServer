using SideWars.Shared.Game;
using SideWarsServer.Game.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideWarsServer.Game.Logic.GameLoop
{
    public class PacketSenderGameLoop : IGameLoop
    {
        private List<(Player, SpellInfo)> spellUses;
        private List<Entity> entityHalts;

        public PacketSenderGameLoop()
        {
            spellUses = new List<(Player, SpellInfo)>();
        }

        public void Update(IGameRoom gameRoom)
        {
            gameRoom.PacketSender.SendMovementPackets();
            gameRoom.PacketSender.SendPlayerSpellUsePackets(spellUses);
            gameRoom.PacketSender.SendEntityHaltPackets(entityHalts);

            spellUses.Clear();
            entityHalts.Clear();
        }

        public void OnSpellUse(SpellInfo info, Player player)
        {
            spellUses.Add((player, info));
        }

        public void OnEntityHaltChange(Entity entity)
        {
            entityHalts.Add(entity);
        }
    }
}
