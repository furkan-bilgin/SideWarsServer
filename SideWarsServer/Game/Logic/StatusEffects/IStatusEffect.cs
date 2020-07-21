using SideWars.Shared.Game;
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
        /// Time period this status effect will be in effect. (In ticks)
        /// </summary>
        int ExpirityPeriod { get; }

        int SpawnTick { get; }

        EntityInfo ApplyEffect(EntityInfo entityInfo);
    }
}
