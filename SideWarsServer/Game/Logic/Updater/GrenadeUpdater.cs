using SideWarsServer.Game.Logic.Projectiles;
using SideWarsServer.Game.Room;

namespace SideWarsServer.Game.Logic.Updater
{
    public class GrenadeUpdater : IEntityUpdater
    {
        public void Update(Entity entity, IGameRoom gameRoom)
        {
            var grenade = (Grenade)entity;
            var room = (BaseGameRoom)gameRoom;

            if (grenade.Location.Y <= 0)
            {
                room.CreateExplosion();
                grenade.Kill();
            }
        }
    }
}
