using Ara3D;
using SideWars.Shared.Game;

namespace SideWarsServer.Game.Logic
{
    public class Projectile : Entity, ITimedDestroy
    {
        public Player Shooter { get; set; }
        public PlayerInfo ShooterInfo { get; set; }
        public ProjectileInfo ProjectileInfo { get; set; }
        public int DestroySeconds { get; set; }

        public Projectile(Vector3 location, ProjectileInfo projectileInfo, Player shooter) : base(projectileInfo)
        {
            DestroySeconds = 4; // TODO

            Shooter = shooter;
            ShooterInfo = shooter.PlayerInfo;
            ProjectileInfo = projectileInfo;
            Team = shooter.Team;

            Location = location;
        }
    }
}
