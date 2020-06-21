using Ara3D;
using SideWars.Shared.Game;
using SideWars.Shared.Packets;
using SideWars.Shared.Physics;
using SideWarsServer.Utils;

namespace SideWarsServer.Game.Logic
{
    public class Bullet : Projectile
    {
        public Bullet(Player shooter, ProjectileInfo projectileInfo) : base(Vector3.Zero, shooter.PlayerInfo, projectileInfo)
        {
            Type = EntityType.Bullet;

            var bulletPosition = shooter.PlayerInfo.BulletPosition.InvertIfRedTeam(shooter.Team);

            Location = shooter.Location + bulletPosition;
            Collider = new SquareCollider(Location, projectileInfo.HitBoxMin, projectileInfo.HitBoxMax);
            Movement = new BulletMovement(shooter.Team, projectileInfo.Speed);
        }
    }
}
