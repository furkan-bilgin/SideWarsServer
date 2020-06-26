using Ara3D;

namespace SideWars.Shared.Physics
{
    public interface ICollider
    {
        /// <summary>
        /// Returns the highest point of the collider. i.e the up of the sphere or square
        /// </summary>
        float GetHighestPoint();
        void UpdateLocation(Vector3 newLocation);
        bool IsColliding(ICollider collider);
    }
}
