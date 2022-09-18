using Ara3D;
using SideWarsServer.Game.Logic.Champions;
using SideWarsServer.Game.Logic.Projectiles;
using SideWarsServer.Game.Room;
using SideWarsServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideWarsServer.Game.Logic.GameLoop
{
    public class EntityMovementGameLoop : IGameLoop
    {
        public void Update(IGameRoom gameRoom)
        {
            foreach (var entity in gameRoom.GetEntities())
            {
                // Continue if the entity is a player. Player movements get updated at PlayerMovementGameLoop.
                if (entity is Player)
                    continue;

                // Continue if this entity is static.
                if (entity.Movement == null)
                    continue;

                // Continue if the movement is halted.
                if (entity.Movement.IsHalted)
                    continue;

                var location = entity.Location;
                entity.Movement.Update(LogicTimer.FixedDelta, ref location);
                entity.Location = location;
            }
        }
    }
}
