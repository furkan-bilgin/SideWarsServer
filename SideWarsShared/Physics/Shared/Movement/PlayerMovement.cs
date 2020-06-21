using Ara3D;
using SideWars.Shared.Packets;

namespace SideWars.Shared.Physics
{
    public class PlayerMovement : IPlayerMovement
    {
        const float GRAVITY = 15;
        const float JUMP_FORCE = 9;

        public float Speed { get; set; }
        public float Horizontal { get; set; }
        public bool Jump { get; set; }
        public EntityTeam Team { get; private set; }

        private float groundLevel;
        private bool onGround;
        private float velocityY;

        public PlayerMovement(EntityTeam Team, ICollider collider, float Speed)
        {
            groundLevel = collider.GetHighestPoint();
            this.Team = Team;
            this.Speed = Speed;
        }

        public void Update(float deltaTime, ref Vector3 location)
        {
            var speed = Team == EntityTeam.Red ? -Speed : Speed;
            var x = location.X;
            var y = location.Y;

            onGround = y == groundLevel;
            if (y > groundLevel)
            {
                velocityY -= GRAVITY * deltaTime;
            }
            else
            {
                velocityY = 0;
            }

            if (onGround && Jump)
            {
                velocityY += JUMP_FORCE;
            }

            var newY = y + velocityY * deltaTime;
            if (newY <= groundLevel) // If we're gonna touch the ground in this frame
            {
                onGround = true;
                y = groundLevel;
            }
            else
            {
                y = newY;
            }
            x += speed * Horizontal * deltaTime;

            location = new Vector3(x, y, location.Z);
        }
    }
}
