using Ara3D;

namespace SideWars.Shared.Game
{
    public partial class ProjectileInfo
    {
        public static ProjectileInfo Bullet => new ProjectileInfo()
        {
            Type = ProjectileType.Bullet,
            Speed = 65,
            Damage = 20,
            HitBoxMin = -(Vector3.One / 4),
            HitBoxMax = (Vector3.One / 4),
            BaseHealth = 1
        };

        public static ProjectileInfo Grenade => new ProjectileInfo()
        {
            Type = ProjectileType.Grenade,
            Speed = 7,
            Damage = 35,
            HitBoxMin = -(Vector3.One / 4),
            HitBoxMax = (Vector3.One / 4),
            BaseHealth = 1
        };
    }
}
