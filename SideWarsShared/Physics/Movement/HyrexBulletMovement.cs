using Ara3D;
using SideWars.Shared.Packets;
using System;

namespace SideWars.Shared.Physics
{
    public class HyrexBulletMovement : BulletMovement
    {
        Vector3? spawnLocation = null;
        private float xVelocity;
        private Random random;
        
        public HyrexBulletMovement(EntityTeam Team, float Speed) : base(Team, Speed)
        { }

        public override void Update(float deltaTime, ref Vector3 location)
        {
            if (spawnLocation == null)
            {
                spawnLocation = location;
                random = new Random(location.X.GetHashCode() * location.Y.GetHashCode() * location.Z.GetHashCode()); // Create a random instance using location as seed.
               
                xVelocity = (float)random.Next(-400, 400) / 100; // Set x velocity
                yVelocity += (float)random.Next(-60, 80) / 100; // Set y velocity  
            }
            
            base.Update(deltaTime, ref location);

            location += new Vector3(xVelocity, 0, 0) * deltaTime;
        }
    }
}
