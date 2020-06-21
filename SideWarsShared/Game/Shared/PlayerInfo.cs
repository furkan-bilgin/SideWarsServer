using Ara3D;

namespace SideWars.Shared.Game
{
    public partial class PlayerInfo
    {
        public ProjectileType ProjectileType { get; set; }
        public PlayerType PlayerType { get; set; }
        public int BaseHealth { get; set; }

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
