using Ara3D;

namespace SideWars.Shared.Game
{
    public partial class PlayerInfo
    {
        public static PlayerInfo Default => new PlayerInfo
        {
            PlayerType = PlayerType.Default,
            ProjectileType = ProjectileType.Bullet,
            BaseHealth = 100,
            AttackSpeed = 300,
            Speed = 5,
            BulletPosition = new Vector3(0, 0.5f, 1),
            HitBoxMin = -Vector3.One,
            HitBoxMax = Vector3.One
        };
    }
}
