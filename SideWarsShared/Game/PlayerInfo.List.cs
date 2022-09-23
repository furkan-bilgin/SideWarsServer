using Ara3D;

namespace SideWars.Shared.Game
{
    public partial class PlayerInfo
    {
        public static PlayerInfo Mark => new PlayerInfo
        {
            PlayerType = ChampionType.Mark,
            ProjectileType = ProjectileType.Bullet,
            BaseHealth = 360,

            AttackTime = 0.3f,
            BulletsPerShoot = 1,
            AmmoCount = 30,
            ReloadTime = 1.5f,

            Speed = 3.5f,

            BulletPosition = new Vector3(0.03f, 0.5f, 1f),
            RunningBulletPosition = new Vector3(0.182f, 0.25f, 1f),

            HitBoxMin = new Vector3(-0.6f, -1, -0.375f),
            HitBoxMax = new Vector3(0.6f, 1, 0.375f)
        };

        public static PlayerInfo Hyrex => new PlayerInfo
        {
            PlayerType = ChampionType.Hyrex,
            ProjectileType = ProjectileType.HyrexBullet,
            BaseHealth = 300,

            AttackTime = 1f,
            BulletsPerShoot = 4,
            AmmoCount = 6,
            ReloadTime = .75f,

            Speed = 3.5f,

            BulletPosition = new Vector3(0.03f, 0.5f, 1f),
            RunningBulletPosition = new Vector3(0.182f, 0.25f, 1f), 

            HitBoxMin = -new Vector3(0.6f, 1, 0.375f),
            HitBoxMax = new Vector3(0.6f, 1, 0.375f)
        };

        public static PlayerInfo Desgama => new PlayerInfo
        {
            PlayerType = ChampionType.Desgama,
            ProjectileType = ProjectileType.DesgamaBullet,
            BaseHealth = 240,

            AttackTime = 1.5f,
            BulletsPerShoot = 1,
            AmmoCount = 6,
            ReloadTime = 1.5f,
            ShootTime = 0.68f,

            Speed = 3.1f,

            BulletPosition = new Vector3(0, 0.35f, 1f),
            RunningBulletPosition = new Vector3(0, 0.35f, 1f),

            HitBoxMin = -new Vector3(0.6f, 1, 0.375f),
            HitBoxMax = new Vector3(0.6f, 1, 0.375f)
        };

        public static PlayerInfo Galacticus => new PlayerInfo
        {
            PlayerType = ChampionType.Galacticus,
            ProjectileType = ProjectileType.GalacticusBullet,
            BaseHealth = 240,

            AttackTime = 0.9f,
            BulletsPerShoot = 1,
            AmmoCount = 6,
            ReloadTime = 1.5f,
            ShootTime = 0.4f,

            Speed = 2.6f,

            BulletPosition = new Vector3(0, 1.35f, 1f),
            RunningBulletPosition = new Vector3(0, 1.35f, 1f),

            HitBoxMin = -new Vector3(0.6f, 1, 0.375f),
            HitBoxMax = new Vector3(0.6f, 1, 0.375f)
        };
    }
}
