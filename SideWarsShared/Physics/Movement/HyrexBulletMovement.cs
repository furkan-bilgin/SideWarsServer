using Ara3D;
using SideWars.Shared.Packets;
using System;
using System.Diagnostics;

namespace SideWars.Shared.Physics
{
    public class HyrexBulletMovement : BulletMovement
    {
        Vector3? spawnLocation = null;
        private float xVelocity;
        private Random random;
        
        public HyrexBulletMovement(EntityTeam Team, float Speed, int Seed) : base(Team, Speed)
        {
            random = new Random(Seed); // Create a random instance using location as seed.

            xVelocity = (float)random.Next(-400, 400) / 100; // Set x velocity
            yVelocity += (float)random.Next(-50, 50) / 100; // Set y velocity  
        }

        public override void Update(float deltaTime, ref Vector3 location)
        {
            base.Update(deltaTime, ref location);

            location += new Vector3(xVelocity, 0, 0) * deltaTime;
        }
    }
}
