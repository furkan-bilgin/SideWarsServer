using Ara3D;
using SideWars.Shared.Packets;
using System;

namespace SideWars.Shared.Physics.Movement
{
    public class GalacticusBulletMovement : BulletMovement
    {
        private Func<Vector3> enemyLocationGetter;
        
        // All in seconds
        private const float FOLLOW_TIME = 2;
        private const float FOLLOW_SPEED = 5;
        private float currentFollowTime;

        public GalacticusBulletMovement(EntityTeam Team, float Speed, Func<Vector3> EnemyLocationGetter) : base(Team, Speed)
        {
            enemyLocationGetter = EnemyLocationGetter;
        }

        public override void Update(float deltaTime, ref Vector3 location)
        {
            if (currentFollowTime >= FOLLOW_TIME)
            {
                xVelocity = 0;
            } 
            else
            {
                var diff = enemyLocationGetter() - location;
                xVelocity = Math.Sign(diff.X) * FOLLOW_SPEED;
                currentFollowTime += deltaTime;
            }

            base.Update(deltaTime, ref location);
        }
    }
}
