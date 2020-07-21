using System;

namespace SideWarsServer.Utils
{
    public static class GameExtensions
    {
        public static float UpByPercentage(this float value, float percentage)
        {
            return value + value * percentage / 100;
        }

        public static float DownByPercentage(this float value, float percentage)
        {
            return value - value * percentage / 100;
        }

        public static int UpByPercentage(this int value, float percentage)
        {
            return Convert.ToInt32(value + value * percentage / 100);
        }

        public static int DownByPercentage(this int value, float percentage)
        {
            return Convert.ToInt32(value - value * percentage / 100);
        }
    }
}
