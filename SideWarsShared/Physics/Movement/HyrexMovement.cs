using Ara3D;
using SideWars.Shared.Game;
using SideWars.Shared.Packets;
using System.Diagnostics;

namespace SideWars.Shared.Physics
{
    public class HyrexMovement : PlayerMovement
    {
        private bool slide;
        /// <summary>
        /// How fast sliding will be compared to normal speed
        /// </summary>
        private const float SLIDE_SPEED_MULTIPLIER = 1.25f;

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
                    slideMultiplier = Horizontal > 0 ? 1 : -1;
                }

                if (stopwatch.ElapsedMilliseconds >= GameConstants.HYREX_FIRST_SPELL_TIME * 1000)
                {
                    slide = false;
                    stopwatch.Stop();
                    return;
                }

                location += new Vector3(slideMultiplier * GetMovementSpeed() * SLIDE_SPEED_MULTIPLIER, 0, 0) * deltaTime;

                CheckSides(deltaTime, ref location);
            }
        }

        public void StartSliding()
        {
            slide = true;
        }
    }
}
