using Ara3D;
using SideWars.Shared.Packets;
using System.Diagnostics;

namespace SideWars.Shared.Physics
{
    public class HyrexMovement : PlayerMovement
    {
        private bool slide;

        private const float SlideTime = 1;
        private Stopwatch stopwatch;
        private float slideMultiplier;

        public HyrexMovement(EntityTeam team, ICollider collider, float speed) : base(team, collider, speed) 
        {
            stopwatch = new Stopwatch();
            stopwatch.Stop();
        }

        public override void Update(float deltaTime, ref Vector3 location)
        {
            if (!slide)
            {
                base.Update(deltaTime, ref location);
            }
            else
            {
                if (!stopwatch.IsRunning)
                {
                    stopwatch.Restart();
                    slideMultiplier = GetMovementMultiplier() > 0 ? 1 : -1;
                }

                if (stopwatch.ElapsedMilliseconds >= SlideTime * 1000)
                {
                    slide = false;
                    return;
                }

                location += new Vector3(slideMultiplier, 0, 0) * deltaTime;

                CheckSides(deltaTime, ref location);
            }
        }

        public void StartSliding()
        {
            slide = true;
        }
    }
}
