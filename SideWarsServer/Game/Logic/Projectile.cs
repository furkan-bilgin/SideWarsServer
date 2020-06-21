using Ara3D;
using SideWars.Shared.Game;

namespace SideWarsServer.Game.Logic
{
    public class Projectile : Entity
    {
        public PlayerInfo ShooterInfo { get; set; }
        public ProjectileInfo ProjectileInfo { get; set; }
        
        public Projectile(Vector3 location, PlayerInfo shooterInfo, ProjectileInfo projectileInfo) : base()
        {
            ShooterInfo = shooterInfo;
            ProjectileInfo = projectileInfo;

            Location = location;
        }
    }
}
