using Ara3D;
using System.Collections.Generic;
using System.IO;

namespace SideWars.Shared.Game
{
    public partial class ProjectileInfo : EntityInfo
    {
        public ProjectileType Type { get; set; }
        public int Damage { get; set; }
    }
}
