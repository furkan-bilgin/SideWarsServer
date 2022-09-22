using System.Diagnostics;

namespace SideWars.Shared.Utils
{
    public class Timer
    {
        public int ElapsedMilliseconds { get; private set; }
        public int PeriodMilliseconds { get; set; }
        private Stopwatch stopwatch;

        public Timer(int periodMilliseconds)
        {
            PeriodMilliseconds = periodMilliseconds;
            
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        public bool CanTick()
        {
            if (stopwatch.ElapsedMilliseconds >= PeriodMilliseconds)
            {
                stopwatch.Restart();
                return true;
            }

            return false;
        }
    }

    public class FloatTimer : Timer
    {
        public float Period { set => PeriodMilliseconds = (int)(value * 1000); }

        public FloatTimer(float period) : base((int)(period * 1000))
        {
        }
    }
}
