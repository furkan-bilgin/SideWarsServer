using Ara3D;
using SideWars.Shared.Physics.Movement;

namespace SideWarsServer.Game.Logic.Projectiles
{
    public class GalacticusBullet : Bullet
    {
        public GalacticusBullet(Player shooter, Player victim) : base(shooter)
        {
            ProjectileInfo = SideWars.Shared.Game.ProjectileInfo.GalacticusBullet;
            Type = SideWars.Shared.Packets.EntityType.GalacticusBullet;
            Movement = new GalacticusBulletMovement(shooter.Team, ProjectileInfo.Speed, () => {
                if (victim != null)
                    return victim.Location;
                return Vector3.Zero;
            });
        }
    }
}
