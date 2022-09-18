using Ara3D;

namespace SideWars.Shared.Physics
{
    public interface IEntityMovement
    {
        float Speed { get; set; }
        bool IsHalted { get; set; }

        void Update(float deltaTime, ref Vector3 location);
    }
}
