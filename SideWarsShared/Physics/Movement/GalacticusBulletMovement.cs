using Ara3D;
using SideWars.Shared.Packets;
using SideWars.Shared.Utils;
using System;

namespace SideWars.Shared.Physics.Movement
{
    public class GalacticusBulletMovement : BulletMovement
    {
        private Func<Vector3> enemyLocationGetter;
        
        // All in seconds
        public const float FOLLOW_TIME = 1.5f;
        private const float FOLLOW_SPEED = 3;
        private float currentFollowTime;

        public GalacticusBulletMovement(EntityTeam Team, float Speed, Func<Vector3> EnemyLocationGetter) : base(Team, Speed)
        {
            enemyLocationGetter = EnemyLocationGetter;
        }

        public override void Update(float deltaTime, ref Vector3 location)
        {
            var enemyLocation = enemyLocationGetter();

            if (currentFollowTime < FOLLOW_TIME && !enemyLocation.Equals(Vector3.Zero))
            {
                var diff = enemyLocation - location;

                var targetXVelocity = Math.Sign(diff.X) * FOLLOW_SPEED;
                currentFollowTime += deltaTime;

                // Change velocity smoothly to prevent jittery movement
                xVelocity = MathOps.Lerp(xVelocity, targetXVelocity, 0.15f);
            }

            base.Update(deltaTime, ref location);
        }
    }
}
