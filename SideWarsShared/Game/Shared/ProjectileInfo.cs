using Ara3D;
using System.Collections.Generic;
using System.IO;

namespace SideWars.Shared.Game
{
    public partial class ProjectileInfo
    {
        public static ProjectileInfo GetProjectileInfo(ProjectileType type)
        {
            switch (type)
            {
                case ProjectileType.Bullet:
                    return Bullet;
            }

            return null;
        }

        public ProjectileType Type { get; set; }
        public float Damage { get; set; }
        public float Speed { get; set; }
        public Vector3 HitBoxMin { get; set; }
        public Vector3 HitBoxMax { get; set; }
    }
}
