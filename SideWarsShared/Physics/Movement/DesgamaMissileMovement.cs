using Ara3D;
using SideWars.Shared.Packets;
using System;
using System.Diagnostics;

namespace SideWars.Shared.Physics
{
    public class DesgamaMissileMovement : BulletMovement
    {
        public DesgamaMissileMovement(EntityTeam Team, float Speed, int angle) : base(Team, Speed)
        {
            var angleInRadians = angle * Math.PI / 180;
            xVelocity += (float)Math.Tan(angleInRadians) * Speed;
        }
    }
}
