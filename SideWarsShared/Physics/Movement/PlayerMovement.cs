using Ara3D;
using SideWars.Shared.Game;
using SideWars.Shared.Packets;

namespace SideWars.Shared.Physics
{
    public class PlayerMovement : IPlayerMovement
    {
        public float Speed { get; set; }
        public float JumpSpeed { get; set; }

        public float Horizontal { get; set; }
        public bool Jump { get; set; }
        public EntityTeam Team { get; private set; }

        private float groundLevel;
        private bool onGround;
        private float velocityY;

        public PlayerMovement(EntityTeam team, ICollider collider, float speed, float jumpSpeed)
        {
            groundLevel = collider.GetHighestPoint();
            this.Team = team;
            this.Speed = speed;
            this.JumpSpeed = jumpSpeed;
        }

        public void Update(float deltaTime, ref Vector3 location)
        {
            var speed = Team == EntityTeam.Red ? -Speed : Speed;
            var x = location.X;
            var y = location.Y;

            onGround = y == groundLevel;
            if (y > groundLevel)
            {
                velocityY -= GameConstants.GRAVITY * deltaTime;
            }
            else
            {
                velocityY = 0;
            }

            if (onGround && Jump)
            {
                velocityY += JumpSpeed;
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
