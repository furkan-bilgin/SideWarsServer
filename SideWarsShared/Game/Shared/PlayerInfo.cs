using Ara3D;

namespace SideWars.Shared.Game
{
    public class PlayerInfo
    {
        public static PlayerInfo Default => new PlayerInfo
        {
            BaseHealth = 100,
            AttackDamage = 20,
            AttackSpeed = 500,
            Speed = 5,
            BulletPosition = new Vector3(0, 1.5f, 1),
            HitBoxMin = -Vector3.One,
            HitBoxMax = Vector3.One
        };

        public float BaseHealth { get; set; }
        public float AttackDamage { get; set; }

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
