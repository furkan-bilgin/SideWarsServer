using Ara3D;
using SideWars.Shared.Packets;
using System;
using System.Diagnostics;

namespace SideWars.Shared.Physics
{
    public class DesgamaMissileMovement : BulletMovement
    {
        private float xVelocity;

        public DesgamaMissileMovement(EntityTeam Team, float Speed, int angle) : base(Team, Speed)
        {
            var angleInRadians = angle * Math.PI / 180;
            xVelocity += (float)Math.Tan(angleInRadians) * Speed;
        }

        public override void Update(float deltaTime, ref Vector3 location)
        {
            base.Update(deltaTime, ref location);

            location += new Vector3(xVelocity, 0, 0) * deltaTime;
        }
    }
}
