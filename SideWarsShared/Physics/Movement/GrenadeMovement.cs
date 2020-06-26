using Ara3D;
using SideWars.Shared.Packets;
using SideWars.Shared.Utils;
using System;

namespace SideWars.Shared.Physics
{
    public class GrenadeMovement : IProjectileMovement
    {
        public float Speed { get; set; }
        public EntityTeam Team { get; set; }
        private Vector3 target;

        public GrenadeMovement(EntityTeam Team, float Speed, Vector3 target)
        {
            this.Team = Team;
            this.Speed = Speed;
            this.target = target;
        }


        public void Update(float deltaTime, ref Vector3 location)
        {
            location = MathUtils.MoveTowards(location, target, Speed * deltaTime)
                .SetY(3);
        }
    }
}
