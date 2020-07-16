using Ara3D;
using SideWars.Shared.Packets;

namespace SideWars.Shared.Physics
{
    public class BulletMovement : IProjectileMovement
    {
        
        public float Speed { get; set; }
        public EntityTeam Team { get; set; }

        private float yVelocity;
        private const float BULLET_GRAVITY = 0.1f;

        public BulletMovement(EntityTeam Team, float Speed)
        {
            this.Team = Team;
            this.Speed = Speed;
        }

        public void Update(float deltaTime, ref Vector3 location)
        {
            var speed = Team == EntityTeam.Red ? -Speed : Speed;
            yVelocity -= BULLET_GRAVITY * deltaTime;
            location = location.SetZ(location.Z + speed * deltaTime)
                .SetY(location.Y + yVelocity);
        }
    }
}
