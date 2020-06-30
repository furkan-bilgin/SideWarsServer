using Ara3D;
using System;

namespace SideWars.Shared.Utils
{
    public static class MathUtils
    {
        public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
        {
            // avoid vector ops because current scripting backends are terrible at inlining
            float toVector_x = target.X - current.X;
            float toVector_y = target.Y - current.Y;
            float toVector_z = target.Z - current.Z;

            float sqdist = toVector_x * toVector_x + toVector_y * toVector_y + toVector_z * toVector_z;

            if (sqdist == 0 || (maxDistanceDelta >= 0 && sqdist <= maxDistanceDelta * maxDistanceDelta))
                return target;
            var dist = (float)Math.Sqrt(sqdist);

            return new Vector3(current.X + toVector_x / dist * maxDistanceDelta,
                current.Y + toVector_y / dist * maxDistanceDelta,
                current.Z + toVector_z / dist * maxDistanceDelta);
        }

        public static ParabolaData GetParabolaData(Vector3 from, Vector3 to, float gravity, float height)
        {
            gravity *= -1;

            float displacementY = to.Y - from.Y;
            Vector3 displacementXZ = new Vector3(to.X - from.X, 0, to.Z - from.Z);
            float time = (float)Math.Sqrt(-2 * height / gravity) + (float)Math.Sqrt(2 * (displacementY - height) / gravity);
            Vector3 velocityY = new Vector3(0, 1, 0) * (float)Math.Sqrt(-2 * gravity * height);
            Vector3 velocityXZ = displacementXZ / time;

            return new ParabolaData(velocityXZ + velocityY * -Math.Sign(gravity), time);
        }
    }

    public struct ParabolaData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public ParabolaData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }

    }
}
