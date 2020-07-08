using SideWarsServer.Game.Logic.Effects;
using SideWarsServer.Game.Logic.Projectiles;
using SideWarsServer.Game.Room;

namespace SideWarsServer.Game.Logic.Updater
{
    public class GrenadeUpdater : IEntityUpdater
    {
        public void Update(Entity entity, IGameRoom gameRoom)
        {
            if (!(entity is Grenade)) // If entity is not grenade, return.
                return;

            var grenade = (Grenade)entity;

            if (grenade.Location.Y <= 0)
            {
                new GrenadeDetonateEffect(grenade).Start(gameRoom);
            }
        }
    }
}
