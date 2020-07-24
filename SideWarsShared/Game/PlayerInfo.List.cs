using Ara3D;

namespace SideWars.Shared.Game
{
    public partial class PlayerInfo
    {
        public static PlayerInfo Mark => new PlayerInfo
        {
            PlayerType = ChampionType.Mark,
            ProjectileType = ProjectileType.Bullet,
            BaseHealth = 120,

            AttackSpeed = 300,
            BulletsPerShoot = 1,
            AmmoCount = 30,
            ReloadTime = 1.5f,

            Speed = 3.5f,
            JumpSpeed = 6,

            BulletPosition = new Vector3(0.03f, 0.5f, 1f),
            RunningBulletPosition = new Vector3(0.182f, 0.25f, 1f),

            HitBoxMin = new Vector3(-0.6f, -1, -0.375f),
            HitBoxMax = new Vector3(0.6f, 1, 0.375f)
        };

        public static PlayerInfo Hyrex => new PlayerInfo
        {
            PlayerType = ChampionType.Hyrex,
            ProjectileType = ProjectileType.HyrexBullet,
            BaseHealth = 100,
            
            AttackSpeed = 1000,
            BulletsPerShoot = 4,
            AmmoCount = 6,
            ReloadTime = .75f,

            Speed = 3.5f,
            JumpSpeed = 6,

            BulletPosition = new Vector3(0.03f, 0.5f, 1f),
            RunningBulletPosition = new Vector3(0.182f, 0.25f, 1f),

            HitBoxMin = -new Vector3(0.6f, 1, 0.375f),
            HitBoxMax = new Vector3(0.6f, 1, 0.375f)
        };
    }
}
