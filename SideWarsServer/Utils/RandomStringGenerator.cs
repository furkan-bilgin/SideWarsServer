using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SideWarsServer.Utils
{
    public class RandomStringGenerator
    {
        private ThreadLocal<Random> threadSafeRandom = new ThreadLocal<Random>(() => new Random());
        private Random _random => threadSafeRandom.Value;

        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        public string RandomString(int size)
        {
            var builder = new StringBuilder(size);
            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            for (int i = 0; i < size; i++)
            {
                builder.Append(allowedChars[RandomNumber(0, allowedChars.Length)]);
            }

            return builder.ToString();
        }
    }
}
