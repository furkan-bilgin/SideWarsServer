using Ara3D;
using SideWarsServer.Game.Logic.Projectiles;
using SideWarsServer.Game.Room;
using SideWars.Shared.Game;
using SideWarsServer.Utils;
using SideWarsServer.Game.Logic.Other;

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

            collidingEntity.Hurt(bullet.ProjectileInfo.Damage);
            if (collidingEntity is Player)
            { 
                room.SpawnParticle(ParticleType.Blood, collidingEntity.Location);
            }
            else if (collidingEntity is Grenade)
            {
                var grenade = (Grenade)collidingEntity;
                new GrenadeDetonateEffect(grenade).Start(room);
            }
            else if (collidingEntity is DesgamaShield)
            {
                // Other bullets have custom death particles in client-side, so we only need to 
                // spawn these
                if (bullet is Bullet || bullet is HyrexBullet)
                    room.SpawnParticle(ParticleType.DesgamaMissileDeath, bullet.Location);
            }

            bullet.Kill();
        }
    }
    
}
