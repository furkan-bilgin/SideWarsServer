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
            BulletPosition = new Vector3(0, 0.5f, 1),
            HitBoxMin = new Vector3(-0.6f, -1, -0.375f),
            HitBoxMax = new Vector3(0.6f, 1, 0.375f)
        };
    }
}
