using Ara3D;
using System;

namespace SideWars.Shared.Physics
{
    public class SquareCollider : ICollider
    {
        public bool IsEnabled { get; set; }
        public Box BoundingBox { get; set; }
        private Vector3 min, max;

        public SquareCollider(Vector3 location, Vector3 min, Vector3 max)
        {
            IsEnabled = true;
            this.min = min;
            this.max = max;

            BoundingBox = new Box(min + location, max + location);
        }

        public void UpdateLocation(Vector3 newLocation)
        {
            BoundingBox = BoundingBox.SetMin(min + newLocation)
                                     .SetMax(max + newLocation);
        }

        public bool IsColliding(ICollider collider)
        {
            if (!IsEnabled)
                return false;

            if (collider is SquareCollider)
            {
                var square = (SquareCollider)collider;
                return BoundingBox.Contains(square.BoundingBox) != ContainmentType.Disjoint;
            }

            return false;
        }

        public float GetHighestPoint()
        {
            return max.Y;
        }
    }
}
