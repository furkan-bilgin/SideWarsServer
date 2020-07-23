using SideWarsServer.Game.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideWarsServer.Game.Logic.GameLoop
{
    public class EntityHealthGameLoop : IGameLoop
    {
        private Dictionary<Entity, int> previousHealths;
        private List<Entity> deadEntities;
        private Action<Entity> sendEntityHealthChangePackets;
        IGameRoom gameRoom;

        public EntityHealthGameLoop(Action<Entity> sendEntityHealthChangePackets)
        {
            this.sendEntityHealthChangePackets = sendEntityHealthChangePackets;
        }

        public void Update(IGameRoom gameRoom)
        {
            this.gameRoom = gameRoom;
            CheckEntityHealthChanges();
            CheckEntityHealths();
        }

        private void CheckEntityHealthChanges()
        {
            foreach (var entity in gameRoom.GetEntities())
            {
                if (!previousHealths.ContainsKey(entity))
                {
                    previousHealths.Add(entity, entity.Health);
                    continue;
                }

                if (entity.Health <= 0)
                {
                    previousHealths.Remove(entity);
                    continue;
                }

                if (previousHealths[entity] != entity.Health)
                {
                    previousHealths[entity] = entity.Health;
                    sendEntityHealthChangePackets(entity);
                }
            }
        }
        private void CheckEntityHealths()
        {
            foreach (var entity in gameRoom.GetEntities())
            {
                if (entity.Health <= 0)
                {
                    deadEntities.Add(entity);
                }
            }

            foreach (var item in deadEntities)
            {
                gameRoom.Entities.Remove(item.Id);
            }
        }
    }
}
