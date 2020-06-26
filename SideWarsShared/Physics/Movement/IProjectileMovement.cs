using Ara3D;
using SideWars.Shared.Packets;

namespace SideWars.Shared.Physics
{
    public interface IProjectileMovement : IEntityMovement
    {
        EntityTeam Team { get; set; }
    }
}
