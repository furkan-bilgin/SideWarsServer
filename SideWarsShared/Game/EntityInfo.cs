using Ara3D;
using System.Collections.Generic;
using System.IO;

namespace SideWars.Shared.Game
{
    public partial class EntityInfo
    {
        public int BaseHealth { get; set; }
        public float Speed { get; set; }
        public Vector3 HitBoxMin { get; set; }
        public Vector3 HitBoxMax { get; set; }
    }
}
