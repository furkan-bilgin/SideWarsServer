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
        private List<(Entity, object)> entityInfoUpdates;

        public PacketSenderGameLoop()
        {
            spellUses = new List<(Player, SpellInfo)>();
            entityInfoUpdates = new List<(Entity, object)>();
        }

        public void Update(IGameRoom gameRoom)
        {
            //gameRoom.PacketSender.SendServerTickPacket();
            gameRoom.PacketSender.SendMovementPackets();
            gameRoom.PacketSender.SendPlayerSpellUsePackets(spellUses);
            gameRoom.PacketSender.SendEntityInfoUpdatePackets(entityInfoUpdates);

            spellUses.Clear();
            entityInfoUpdates.Clear();
        }

        public void OnSpellUse(SpellInfo info, Player player)
        {
            spellUses.Add((player, info));
        }

        public void OnEntityInfoChange(Entity entity, Dictionary<string, object> newEntityInfo)
        {
            entityInfoUpdates.Add((entity, newEntityInfo));
        }
    }
}
