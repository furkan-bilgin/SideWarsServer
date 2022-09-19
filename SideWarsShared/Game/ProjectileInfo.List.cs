using Ara3D;

namespace SideWars.Shared.Game
{
    public partial class ProjectileInfo
    {
        public static ProjectileInfo Bullet => new ProjectileInfo()
        {
            Type = ProjectileType.Bullet,
            Speed = 55,
            Damage = 12,
            HitBoxMin = -Vector3.One / 1.75f,
            HitBoxMax = Vector3.One / 1.75f,
            BaseHealth = 1
        };

        public static ProjectileInfo HyrexBullet => new ProjectileInfo()
        {
            Type = ProjectileType.HyrexBullet,
            Speed = 55,
            Damage = 4,
            HitBoxMin = -Vector3.One / 1.75f,
            HitBoxMax = Vector3.One / 1.75f,
            BaseHealth = 1
        };

        public static ProjectileInfo Grenade => new ProjectileInfo()
        {
            Type = ProjectileType.Grenade,
            Speed = 7,
            Damage = 20,
            HitBoxMin = -Vector3.One / 1.25f,
            HitBoxMax = Vector3.One / 1.25f,
            BaseHealth = 1
        };

        public static ProjectileInfo DesgamaBullet => new ProjectileInfo()
        {
            Type = ProjectileType.DesgamaBullet,
            Speed = 30,
            Damage = 15,
            HitBoxMin = -Vector3.One / 1.5f,
            HitBoxMax = Vector3.One / 1.5f,
            BaseHealth = 1
        };

        public static ProjectileInfo DesgamaMissile => new ProjectileInfo()
        {
            Type = ProjectileType.DesgamaMissile,
            Speed = 35,
            Damage = 10,
            HitBoxMin = -Vector3.One / 1.25f,
            HitBoxMax = Vector3.One / 1.25f,
            BaseHealth = 1
        };


        public static ProjectileInfo GalacticusBullet => new ProjectileInfo()
        {
            Type = ProjectileType.DesgamaMissile,
            Speed = 25,
            Damage = 8,
            HitBoxMin = -Vector3.One / 1.25f,
            HitBoxMax = Vector3.One / 1.25f,
            BaseHealth = 1
        };
    }
}
