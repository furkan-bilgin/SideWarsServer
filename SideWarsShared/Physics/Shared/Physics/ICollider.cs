using Ara3D;

namespace SideWars.Shared.Physics
{
    public interface ICollider
    {
        /// <summary>
        /// Returns the lowest point of the collider. i.e the bottom of the sphere or square
        /// </summary>
        float GetLowestPoint();
        void UpdateLocation(Vector3 newLocation);
        bool IsColliding(ICollider collider);
    }
}
