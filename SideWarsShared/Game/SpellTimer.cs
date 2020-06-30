using SideWars.Shared.Utils;
using System.Collections.Generic;
using System.Diagnostics;

namespace SideWars.Shared.Game
{
    public class SpellTimer
    {
        private Dictionary<SpellType, Timer> timerList;

        public SpellTimer()
        {
            timerList = new Dictionary<SpellType, Timer>();
        }

        public bool CanCast(SpellInfo info)
        {
            Timer timer;
            if (!timerList.ContainsKey(info.Type))
            {
                timer = new Timer(info.Cooldown);
                timerList.Add(info.Type, timer);
                return true;
            }

            timer = timerList[info.Type];
            return timer.CanTick();
        }
    }
}
