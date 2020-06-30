using System.Diagnostics;

namespace SideWars.Shared.Utils
{
    public class Timer
    {
        public int ElapsedMilliseconds { get; private set; }
        private int PeriodMilliseconds { get; set; }
        private Stopwatch stopwatch;

        public Timer(int periodMilliseconds)
        {
            PeriodMilliseconds = periodMilliseconds;
            stopwatch = new Stopwatch();
        }

        public bool CanTick()
        {
            if (!stopwatch.IsRunning)
            {
                stopwatch.Start();
                return true;
            }

            if (stopwatch.ElapsedMilliseconds >= PeriodMilliseconds)
            {
                stopwatch.Restart();
                return true;
            }

            return false;
        }
    }
}
