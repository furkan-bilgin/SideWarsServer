using Ara3D;

namespace SideWars.Shared.Game
{
    public partial class ProjectileInfo
    {
        public static ProjectileInfo Bullet => new ProjectileInfo()
        {
            Type = ProjectileType.Bullet,
            Speed = 80,
            Damage = 12,
            HitBoxMin = -(Vector3.One / 3),
            HitBoxMax = (Vector3.One / 3),
            BaseHealth = 1
        };

        public static ProjectileInfo HyrexBullet => new ProjectileInfo()
        {
            Type = ProjectileType.HyrexBullet,
            Speed = 75,
            Damage = 4,
            HitBoxMin = -(Vector3.One / 3),
            HitBoxMax = (Vector3.One / 3),
            BaseHealth = 1
        };

        public static ProjectileInfo Grenade => new ProjectileInfo()
        {
            Type = ProjectileType.Grenade,
            Speed = 7,
            Damage = 20,
            HitBoxMin = -(Vector3.One / 2),
            HitBoxMax = (Vector3.One / 2),
            BaseHealth = 1
        };
    }
}
