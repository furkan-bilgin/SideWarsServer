using Ara3D;
using SideWarsServer.Game.Logic.Projectiles;
using SideWarsServer.Game.Room;
using SideWars.Shared.Game;

namespace SideWarsServer.Game.Logic.Effects
{
    public class GrenadeDetonateEffect : IEffect
    {
        private Grenade grenade;
        private bool midAir;

        public GrenadeDetonateEffect(Grenade grenade, bool midAir = false)
        {
            this.grenade = grenade;
            this.midAir = midAir;
        }

        public void Start(IGameRoom room)
        {
            grenade.Kill();
            new ExplosionEffect(grenade.Location, 5, grenade.ProjectileInfo.Damage).Start(room);

            room.SpawnParticle(midAir ? ParticleType.MidAirExplosion : ParticleType.Explosion, grenade.Location, null);
        }
    }
}
