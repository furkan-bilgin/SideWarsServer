using Ara3D;
using SideWars.Shared.Utils;
using System;
using System.Collections.Generic;
using System.IO;

namespace SideWars.Shared.Game
{
    public partial class EntityInfo : ICloneable
    {
        public int BaseHealth { get; set; }
        public float Speed { get; set; }
        public Vector3 HitBoxMin { get; set; }
        public Vector3 HitBoxMax { get; set; }

        public object Clone()
        {
            return ObjectCloner.Clone(this);
        }
    }
}
