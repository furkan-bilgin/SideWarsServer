using Ara3D;
using SideWars.Shared.Packets;

namespace SideWars.Shared.Physics
{
    public class BulletMovement : IProjectileMovement
    {
        public float Speed { get; set; }
        public EntityTeam Team { get; set; }
        public bool IsHalted { get; set; }

        protected float yVelocity;
        protected float xVelocity;
        private const float BULLET_GRAVITY = 2.5f;

        public BulletMovement(EntityTeam Team, float Speed)
        {
            this.Team = Team;
            this.Speed = Speed;
        }

        public virtual void Update(float deltaTime, ref Vector3 location)
        {
            var speed = Team == EntityTeam.Red ? -Speed : Speed;
            yVelocity -= BULLET_GRAVITY * deltaTime;

            location += new Vector3(xVelocity, yVelocity, speed) * deltaTime;
        }
    }
}
