using Ara3D;
using System;

namespace SideWars.Shared.Physics
{
    public class SquareCollider : ICollider
    {
        public Box BoundingBox { get; set; }
        private Vector3 min, max;

        public SquareCollider(Vector3 location, Vector3 min, Vector3 max)
        {
            this.min = min;
            this.max = max;

            BoundingBox = new Box(min + location, max + location);
        }

        public void UpdateLocation(Vector3 newLocation)
        {
            BoundingBox.SetMin(min + newLocation);
            BoundingBox.SetMax(max + newLocation);
        }

        public bool IsColliding(ICollider collider)
        {
            if (collider is SquareCollider)
            {
                var square = (SquareCollider)collider;
                return BoundingBox.Contains(square.BoundingBox) != ContainmentType.Disjoint;
            }

            return false;
        }

        public float GetLowestPoint()
        {
            return min.Y;
        }
    }
}
