using Ara3D;
using SideWars.Shared.Packets;
using System;
using System.Diagnostics;

namespace SideWars.Shared.Physics
{
    public class HyrexBulletMovement : BulletMovement
    {
        Vector3? spawnLocation = null;
        private Random random;
        
        public HyrexBulletMovement(EntityTeam Team, float Speed, int Seed) : base(Team, Speed)
        {
            random = new Random(Seed); // Create a random instance using location as seed.

            xVelocity = (float)random.Next(-250, 250) / 100; // Set x velocity
            yVelocity += (float)random.Next(-50, 50) / 100; // Set y velocity  
        }
    }
}
