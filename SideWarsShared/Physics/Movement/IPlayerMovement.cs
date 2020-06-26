using Ara3D;

namespace SideWars.Shared.Physics
{
    public interface IPlayerMovement : IEntityMovement
    {
        float Horizontal { get; set; }
        bool Jump { get; set; }
    }
}
