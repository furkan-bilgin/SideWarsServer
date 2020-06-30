using Ara3D;
using SideWars.Shared.Game;
using SideWars.Shared.Packets;
using SideWars.Shared.Physics;
using SideWarsServer.Utils;

namespace SideWarsServer.Game.Logic.Projectiles
{
    public class Grenade : Projectile
    {
        public Vector3 Target { get; private set; }
        public Grenade(Player shooter) : base(Vector3.Zero, ProjectileInfo.Grenade, shooter)
        {
            Type = EntityType.Grenade;
            Target = new Vector3(shooter.Location.X, 0, 15);

            var bulletPosition = shooter.PlayerInfo.BulletPosition.InvertIfRedTeam(shooter.Team);
            
            Location = shooter.Location + bulletPosition;
            Collider = new SquareCollider(Location, ProjectileInfo.HitBoxMin, ProjectileInfo.HitBoxMax);
            Movement = new GrenadeMovement(shooter.Team, Target, Location);
        }
    }
}
