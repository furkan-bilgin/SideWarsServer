using Ara3D;
using SideWars.Shared.Game;
using SideWars.Shared.Packets;
using SideWars.Shared.Physics;
using SideWarsServer.Networking;
using SideWarsServer.Utils;
using System.Collections.Generic;

namespace SideWarsServer.Game.Logic.Projectiles
{
    public class HyrexBullet : Bullet
    {
        public int BulletSeed { get; private set; }

        public HyrexBullet(Player shooter) : base(shooter)
        {
            BulletSeed = RandomTool.Current.Int(0, 1500);

            Type = EntityType.HyrexBullet;
            ProjectileInfo = ProjectileInfo.HyrexBullet;

            Collider = new SquareCollider(Location, ProjectileInfo.HitBoxMin, ProjectileInfo.HitBoxMax);
            Movement = new HyrexBulletMovement(shooter.Team, ProjectileInfo.Speed, BulletSeed);
        }

        public override void Packetify(ref List<ushort> data, ref List<float> bigData, PlayerConnection connection)
        {
            bigData.Add(BulletSeed);
        }
    }
}
