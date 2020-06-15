using Ara3D;

namespace SideWarsServer.Game.Logic.Physics
{
    public interface ICollider
    {
        void UpdateLocation(Vector3 newLocation);
        bool IsColliding(ICollider collider);
    }
}
