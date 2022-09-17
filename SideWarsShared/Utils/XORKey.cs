using System;
using System.Collections.Generic;

namespace SideWars.Shared.Utils
{
    public class XORKey
    {
        const int KEY_SIZE = 512;

        public static byte[] Generate(string matchToken)
        {
            var rand = new Random(matchToken.GetHashCode());
            var key = new List<byte>();

            for (int i = 0; i < KEY_SIZE; i++)
            {
                var randChar = matchToken[rand.Next(0, matchToken.Length)];
                key.Add(Convert.ToByte(randChar));
            }

            return key.ToArray();
        }
    }
}
