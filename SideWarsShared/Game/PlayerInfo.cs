using Ara3D;
using SideWars.Shared.Utils;

namespace SideWars.Shared.Game
{
    public partial class PlayerInfo : EntityInfo
    {
        public float JumpSpeed { get; set; }

        public ProjectileType ProjectileType { get; set; }
        public ChampionType PlayerType { get; set; }

        /// <summary>
        /// Player attack speed in milliseconds.
        /// </summary>
        public int AttackSpeed { get; set; } 

        public int BulletsPerShoot { get; set; }

        public int AmmoCount { get; set; }
        public float ReloadTime { get; set; }

        public Vector3 BulletPosition { get; set; }
        public Vector3 RunningBulletPosition { get; set; }

        public new object Clone()
        {
            return ObjectCloner.Clone(this);
        }
    }
}
