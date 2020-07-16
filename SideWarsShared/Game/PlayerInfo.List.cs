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
            Speed = 3.5f,
            JumpSpeed = 6,
            
            BulletPosition = new Vector3(0.03f, 0.5f, 1f),
            RunningBulletPosition = new Vector3(0.182f, 0.25f, 1f),

            HitBoxMin = new Vector3(-0.6f, -1, -0.375f),
            HitBoxMax = new Vector3(0.6f, 1, 0.375f)
        };
    }
}
