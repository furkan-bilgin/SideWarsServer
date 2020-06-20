using Ara3D;
using SideWars.Shared.Packets;

namespace SideWars.Shared.Physics
{
    public class BulletMovement : IProjectileMovement
    {
        public float Speed { get; set; }
        public EntityTeam Team { get; set; }
        
        public void Update(float deltaTime, ref Vector3 location)
        {
            location = location.SetZ(location.Z + Speed * deltaTime);
        }
    }
}
