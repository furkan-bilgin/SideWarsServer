using SideWars.Shared.Game;
using SideWarsServer.Game.Logic.Champions;
using SideWarsServer.Game.Logic.Projectiles;

namespace SideWarsServer.Game.Logic
{
    public class ProjectileSpawner
    {
        public Projectile SpawnProjectile(ProjectileType type, Player shooter)
        {
            Projectile projectile = null;

            switch (type)
            {
                case ProjectileType.Bullet:
                    projectile = new Bullet(shooter);
                    break;
                case ProjectileType.HyrexBullet:
                    projectile = new HyrexBullet(shooter);
                    break;
                case ProjectileType.Grenade:
                    projectile = new Grenade(shooter);
                    break;
                case ProjectileType.DesgamaBullet:
                    projectile = new DesgamaBullet(shooter);
                    break;
                case ProjectileType.GalacticusBullet:
                    projectile = new GalacticusBullet(shooter);
                    break;
            }

            return projectile;
        }
    }
}
