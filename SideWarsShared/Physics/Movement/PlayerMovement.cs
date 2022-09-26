using Ara3D;
using SideWars.Shared.Game;
using SideWars.Shared.Packets;
using SideWars.Shared.Utils;

namespace SideWars.Shared.Physics
{
    public class PlayerMovement : IPlayerMovement
    {
        public float Speed { get; set; }

        public float Horizontal { get; set; }
        public EntityTeam Team { get; private set; }


        public PlayerMovement(EntityTeam team, ICollider collider, float speed)
        {
            this.Team = team;
            this.Speed = speed;
        }

        public virtual void Update(float deltaTime, ref Vector3 location)
        {
            // Clamp horizontal speed between 1 and -1
            // -1 <= x <= 1 where x = Horizontal
            Horizontal = MathOps.Min(1, Horizontal);
            Horizontal = MathOps.Max(-1, Horizontal);

            location += new Vector3(GetMovementSpeed() * Horizontal, 0, 0) * deltaTime;
            location.TruncateVec();

            CheckSides(deltaTime, ref location);
        }

        protected void CheckSides(float deltaTime, ref Vector3 location)
        {
            // TODO: Limit side movement.
        }

        protected float GetMovementSpeed()
        {
            return Team == EntityTeam.Red ? -Speed : Speed;
        }
    }
}
