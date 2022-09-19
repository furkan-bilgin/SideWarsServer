using SideWars.Shared.Physics;

namespace SideWarsServer.Game.Logic.Projectiles
{
    public class DesgamaBullet : Bullet
    {
        public DesgamaBullet(Player shooter) : base(shooter)
        {
            ProjectileInfo = SideWars.Shared.Game.ProjectileInfo.DesgamaBullet;
            Type = SideWars.Shared.Packets.EntityType.DesgamaBullet;
            Movement = new BulletMovement(shooter.Team, ProjectileInfo.Speed);
        }
    }
}
