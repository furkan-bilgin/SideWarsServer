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
        public EntityTeam Team { get; private set; }

        public PlayerMovement(EntityTeam team, ICollider collider, float speed)
        {
            this.Team = team;
            this.Speed = speed;
        }

        public virtual void Update(float deltaTime, ref Vector3 location)
        {
            location += new Vector3(GetMovementMultiplier() * Horizontal, 0, 0) * deltaTime;
        }

        protected void CheckSides(float deltaTime, ref Vector3 location)
        {
            // TODO: Limit side movement.
        }

        protected float GetMovementMultiplier()
        {
            return Team == EntityTeam.Red ? -Speed : Speed;
        }
    }
}
