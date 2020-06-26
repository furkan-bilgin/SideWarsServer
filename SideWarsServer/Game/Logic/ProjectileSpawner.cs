using SideWars.Shared.Game;
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

                case ProjectileType.Grenade:
                    projectile = new Grenade(shooter);
                    break;
            }

            return projectile;
        }
    }
}
