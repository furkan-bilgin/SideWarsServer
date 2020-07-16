using Ara3D;
using SideWars.Shared.Game;
using SideWars.Shared.Packets;
using SideWars.Shared.Physics;
using SideWarsServer.Utils;

namespace SideWarsServer.Game.Logic.Projectiles
{
    public class HyrexBullet : Bullet
    {
        public HyrexBullet(Player shooter) : base(shooter)
        {
            Type = EntityType.HyrexBullet;
            ProjectileInfo = ProjectileInfo.HyrexBullet;

            var randomOffset = new Vector3(RandomTool.Float(-0.04f, 0.04f), RandomTool.Float(-0.04f, 0.04f), RandomTool.Float(-0.04f, 0.04f)); // Since bullet randomisation uses position as seed, we could randomise the position a little bit
            Location += randomOffset;                                                                                                                    // to make fix some stupid stuff. Like shooting bullets in the same rotation when not moving.

            Movement = new HyrexBulletMovement(shooter.Team, ProjectileInfo.Speed);
        }
    }
}
