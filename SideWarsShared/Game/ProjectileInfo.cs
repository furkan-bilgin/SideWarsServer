using Ara3D;
using SideWars.Shared.Utils;
using System.Collections.Generic;
using System.IO;

namespace SideWars.Shared.Game
{
    public partial class ProjectileInfo : EntityInfo
    {
        public ProjectileType Type { get; set; }
        public int Damage { get; set; }

        public new object Clone()
        {
            return ObjectCloner.Clone(this);
        }
    }
}
