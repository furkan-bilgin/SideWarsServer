using SideWars.Shared.Game;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideWars.Shared.Game
{
    public class SpellTimer
    {
        private Dictionary<SpellType, Stopwatch> timerList;

        public SpellTimer()
        {
            timerList = new Dictionary<SpellType, Stopwatch>();
        }

        public bool CanCast(SpellInfo info)
        {
            Stopwatch stopwatch = null;
            if (!timerList.ContainsKey(info.Type))
            {
                stopwatch = new Stopwatch();
                stopwatch.Start();

                timerList.Add(info.Type, stopwatch);
                return true;
            }

            stopwatch = timerList[info.Type];
            if (stopwatch.ElapsedMilliseconds >= info.Cooldown * 1000)
            {
                stopwatch.Reset();
                stopwatch.Start();

                return true;
            }

            return false;
        }
    }
}
