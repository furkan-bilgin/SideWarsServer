using SideWars.Shared.Physics;
using SideWarsServer.Networking;
using System.Collections.Generic;

namespace SideWarsServer.Game.Logic.Projectiles
{
    public class DesgamaMissile : Bullet
    {
        private int angle;
        public DesgamaMissile(Player shooter, int angle) : base(shooter)
        {
            this.angle = angle;
            ProjectileInfo = SideWars.Shared.Game.ProjectileInfo.DesgamaMissile;
            Type = SideWars.Shared.Packets.EntityType.DesgamaMissile;

            Collider = new SquareCollider(Location, ProjectileInfo.HitBoxMin, ProjectileInfo.HitBoxMax);
            Movement = new DesgamaMissileMovement(shooter.Team, ProjectileInfo.Speed, angle);
            Location += new Ara3D.Vector3(0, -0.4f, 0);
        }

        public override void Packetify(ref List<ushort> data, ref List<float> bigData, PlayerConnection connection)
        {
            bigData.Add(angle);
        }
    }
}
