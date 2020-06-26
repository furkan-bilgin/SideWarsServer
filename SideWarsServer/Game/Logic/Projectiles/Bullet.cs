using Ara3D;
using SideWars.Shared.Game;
using SideWars.Shared.Packets;
using SideWars.Shared.Physics;
using SideWarsServer.Utils;

namespace SideWarsServer.Game.Logic.Projectiles
{
    public class Bullet : Projectile
    {
        public Bullet(Player shooter) : base(Vector3.Zero, ProjectileInfo.Bullet, shooter)
        {
            Type = EntityType.Bullet;

            var bulletPosition = shooter.PlayerInfo.BulletPosition.InvertIfRedTeam(shooter.Team);

            Location = shooter.Location + bulletPosition;
            Collider = new SquareCollider(Location, ProjectileInfo.HitBoxMin, ProjectileInfo.HitBoxMax);
            Movement = new BulletMovement(shooter.Team, ProjectileInfo.Speed);
        }
    }
}
