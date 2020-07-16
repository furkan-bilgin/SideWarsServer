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

                    if (shooter is Hyrex)
                    {
                        projectile = new HyrexBullet(shooter);
                    }
                    else
                    {
                        projectile = new Bullet(shooter);
                    }

                    break;

                case ProjectileType.Grenade:
                    projectile = new Grenade(shooter);
                    break;
            }

            return projectile;
        }
    }
}
