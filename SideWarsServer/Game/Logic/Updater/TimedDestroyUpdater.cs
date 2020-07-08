using SideWarsServer.Game.Logic.Projectiles;
using SideWarsServer.Game.Room;
using SideWarsServer.Utils;

namespace SideWarsServer.Game.Logic.Updater
{
    public class TimedDestroyUpdater : IEntityUpdater
    {
        public void Update(Entity entity, IGameRoom gameRoom)
        {
            if (!(entity is ITimedDestroy)) // If entity is not grenade, return.
                return;

            var timedDestroy = (ITimedDestroy)entity;
            if (gameRoom.Tick - entity.BirthTick >= LogicTimer.FramesPerSecond * timedDestroy.DestroySeconds) // If the time has passed
            {
                entity.Kill(); // Kill the entity
            }
        }
    }
}
