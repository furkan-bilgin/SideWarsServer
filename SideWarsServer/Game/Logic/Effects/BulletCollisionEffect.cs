using Ara3D;
using SideWarsServer.Game.Logic.Projectiles;
using SideWarsServer.Game.Room;
using SideWars.Shared.Game;

namespace SideWarsServer.Game.Logic.Effects
{
    public class BulletCollisionEffect : IEffect
    {
        private Bullet bullet;
        private Entity collidingEntity;

        public BulletCollisionEffect(Bullet bullet, Entity collidingEntity)
        {
            this.bullet = bullet;
            this.collidingEntity = collidingEntity;
        }

        public void Start(IGameRoom room)
        {
            if (bullet.Team == collidingEntity.Team)
                return;

            if (collidingEntity is Player)
            { 
                collidingEntity.Hurt(bullet.ProjectileInfo.Damage);
                room.SpawnParticle(ParticleType.Blood, collidingEntity.Location);
            }
            else if (collidingEntity is Grenade)
            {
                var grenade = (Grenade)collidingEntity;
                new GrenadeDetonateEffect(grenade).Start(room);
            }

            bullet.Kill();
        }
    }
    
}
