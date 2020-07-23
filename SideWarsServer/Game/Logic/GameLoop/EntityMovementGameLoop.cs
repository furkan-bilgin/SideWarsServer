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
                var location = entity.Location;
                entity.Movement.Update(LogicTimer.FixedDelta, ref location);
                if (entity is HyrexBullet)
                    Logger.Info(location.ToString());

                entity.Location = location;
            }
        }
    }
}
