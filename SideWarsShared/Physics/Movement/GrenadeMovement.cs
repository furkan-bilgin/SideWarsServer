using Ara3D;
using SideWars.Shared.Game;
using SideWars.Shared.Packets;
using SideWars.Shared.Utils;
using System;

namespace SideWars.Shared.Physics
{
    public class GrenadeMovement : IProjectileMovement
    {
        const float HEIGHT = 2f;

        public float Speed { get; set; }
        public EntityTeam Team { get; set; }
        private Vector3 target;
        private ParabolaData parabolaData;
        private Vector3 velocity;

        public GrenadeMovement(EntityTeam Team, Vector3 target, Vector3 location)
        {
            this.Speed = 0;
            this.Team = Team;
            this.target = target;
            parabolaData = MathUtils.GetParabolaData(location, target, GameConstants.GRAVITY, HEIGHT);
            velocity = parabolaData.initialVelocity;
        }


        public void Update(float deltaTime, ref Vector3 location)
        {
            location += velocity * deltaTime;
            velocity = velocity.SetY(velocity.Y - GameConstants.GRAVITY * deltaTime);
        }
    }
}
