using Ara3D;

namespace SideWars.Shared.Physics
{
    public interface IPlayerMovement
    {
        float Horizontal { get; set; }
        bool Jump { get; set; }

        void Update(float deltaTime, ref Vector3 location);
    }
}
