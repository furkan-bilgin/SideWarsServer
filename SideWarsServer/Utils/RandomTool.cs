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

        public string String(int size)
        {
            var builder = new StringBuilder(size);
            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            for (int i = 0; i < size; i++)
            {
                builder.Append(allowedChars[Current.Int(0, allowedChars.Length - 1)]);
            }

            return builder.ToString();
        }

    }
}
