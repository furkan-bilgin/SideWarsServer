using Ara3D;

namespace SideWars.Shared.Game
{
    public class PlayerInfo
    {
        public static PlayerInfo Default => new PlayerInfo
        {
            ProjectileType = ProjectileType.Default,
            BaseHealth = 100,
            AttackSpeed = 500,
            Speed = 5,
            BulletPosition = new Vector3(0, 1.5f, 1),
            HitBoxMin = -Vector3.One,
            HitBoxMax = Vector3.One
        };

        public ProjectileType ProjectileType { get; set; }
        public PlayerType PlayerType { get; set; }
        public float BaseHealth { get; set; }

        /// <summary>
        /// Player attack speed in milliseconds.
        /// </summary>
        public int AttackSpeed { get; set; }
        public float Speed { get; set; }

        public Vector3 BulletPosition { get; set; }
        public Vector3 HitBoxMin { get; set; }
        public Vector3 HitBoxMax { get; set; }
    }
}
