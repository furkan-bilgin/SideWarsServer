using Ara3D;
using SideWars.Shared.Game;
using SideWars.Shared.Packets;
using SideWars.Shared.Physics;
using SideWarsServer.Networking;
using SideWarsServer.Utils;
using System.Collections.Generic;

namespace SideWarsServer.Game.Logic.Projectiles
{
    public class Grenade : Projectile
    {
        public Vector3 Target { get; private set; }
        public Grenade(Player shooter) : base(Vector3.Zero, ProjectileInfo.Grenade, shooter)
        {
            var playerMovement = (PlayerMovement)shooter.Movement;
            var addX = (playerMovement.Horizontal * playerMovement.Speed).InvertIfRedTeam(Team);

            Type = EntityType.Grenade;
            Target = new Vector3(shooter.Location.X + addX, 0, Team == EntityTeam.Blue ? 14 : -2); //TODO

            var bulletPosition = shooter.PlayerInfo.BulletPosition.InvertIfRedTeam(shooter.Team);
            
            Location = shooter.Location + bulletPosition;
            Collider = new SquareCollider(Location, ProjectileInfo.HitBoxMin, ProjectileInfo.HitBoxMax);
            Movement = new GrenadeMovement(shooter.Team, Target, Location);
        }

        public override void Packetify(ref List<ushort> data, ref List<float> bigData, PlayerConnection connection)
        {
            bigData.Add(Target.X);
            bigData.Add(Target.Y);
            bigData.Add(Target.Z);
        }
    }
}
