using System;

namespace SideWarsServer.Utils
{
    public static class Functions
    {
        public static sbyte AsSByte(float var)
        {
            var = Ara3D.MathOps.Clamp(var, -1, 1);
            return Convert.ToSByte(var * 127.0f);
        }

        public static float AsFloat(sbyte var)
        {
            return var / 127.0f;
        }
    }
}
