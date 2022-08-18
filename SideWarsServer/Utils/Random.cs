using Ara3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SideWarsServer.Utils
{
    public class RandomTool
    {
        public static RandomTool Current => threadLocal.Value;
        public static ThreadLocal<RandomTool> threadLocal = new ThreadLocal<RandomTool>(() => new RandomTool());
        private Random random;

        public RandomTool()
        {
            random = new Random();
        }

        public float Float(float min, float max)
        {
            return Math.Max(min, (float)random.NextDouble() * max);
        }

        public int Int(int min, int max)
        {
            return (int)Float(min, max);
        }
    }
}
