using Ara3D;

namespace SideWars.Shared.Game
{
    public partial class PlayerInfo : EntityInfo
    {
        public ProjectileType ProjectileType { get; set; }
        public PlayerType PlayerType { get; set; }

        /// <summary>
        /// Player attack speed in milliseconds.
        /// </summary>
        public int AttackSpeed { get; set; }

        public Vector3 BulletPosition { get; set; }
    }
}
