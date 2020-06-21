using Ara3D;

namespace SideWars.Shared.Game
{
    public class ProjectileInfo
    {
        public static ProjectileInfo Default => new ProjectileInfo()
        {
            Speed = 10,
            Damage = 20
        };

        public ProjectileType Type { get; set; }
        public float Damage { get; set; }
        public float Speed { get; set; }
    }
}
