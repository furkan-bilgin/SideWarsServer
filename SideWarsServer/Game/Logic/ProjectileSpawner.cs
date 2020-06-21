using SideWars.Shared.Game;

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
                    projectile = new Bullet(shooter, ProjectileInfo.GetProjectileInfo(type));
                    break;
            }

            return projectile;
        }
    }
}
