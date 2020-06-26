using Ara3D;
using System;

namespace SideWars.Shared.Utils
{
    public static class MathUtils
    {
        public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
        {
            Vector3 a = target - current;
            float magnitude = (float)Math.Sqrt(a.X * a.X + a.Y * a.Y + a.Z * a.Z);
            if (magnitude <= maxDistanceDelta || magnitude == 0f)
            {
                return target;
            }
            return current + a / magnitude * maxDistanceDelta;
        }
    }
}
