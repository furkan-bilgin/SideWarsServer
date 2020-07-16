using Ara3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideWarsServer.Utils
{
    public static class RandomTool
    {
        static Random random = new Random();

        public static float Float(float min, float max)
        {
            return Math.Max(min, (float)random.NextDouble() * max);
        }

        public static int Int(int min, int max)
        {
            return (int)Float(min, max);
        }
    }
}
