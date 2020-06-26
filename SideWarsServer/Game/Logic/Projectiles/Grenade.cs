using Ara3D;
using SideWars.Shared.Game;
using SideWars.Shared.Packets;
using SideWars.Shared.Physics;
using SideWarsServer.Utils;

namespace SideWarsServer.Game.Logic.Projectiles
{
    public class Grenade : Projectile
    {
        public Grenade(Player shooter) : base(Vector3.Zero, ProjectileInfo.Grenade, shooter)
        {
            Type = EntityType.Grenade;

            var bulletPosition = shooter.PlayerInfo.BulletPosition.InvertIfRedTeam(shooter.Team);

            Location = shooter.Location + bulletPosition;
            Collider = new SquareCollider(Location, ProjectileInfo.HitBoxMin, ProjectileInfo.HitBoxMax);
            Movement = new GrenadeMovement(shooter.Team, ProjectileInfo.Speed, Location.Y, 3.7f, 10);
        }
    }
}
