using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideWarsServer.Game.Logic.StatusEffects
{
    public interface IStatusEffect
    {
        /// <summary>
        /// Time period this status effect will be in effect. (In seconds)
        /// </summary>
        float Period { get; }

        void Start(Entity entity);
        void Stop();
    }
}
