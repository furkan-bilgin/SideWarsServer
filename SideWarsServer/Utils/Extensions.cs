using Ara3D;
using SideWars.Shared.Packets;
using System;

namespace SideWarsServer.Utils
{
    public static class Extensions
    {
        public static Vector3 InvertIfRedTeam(this Vector3 vec, EntityTeam team)
        {
            return vec.SetZ(team == EntityTeam.Red ? -vec.Z : vec.Z);
        }

        public static float InvertIfRedTeam(this float val, EntityTeam team)
        {
            return team == EntityTeam.Red ? -val : val;
        }

        public static int SecondsToTicks(this float val)
        {
            return Convert.ToInt32(val * LogicTimer.FramesPerSecond);
        }

        public static EntityTeam GetOppositeTeam(this EntityTeam team)
        {
            if (team == EntityTeam.None)
                return team;

            return team == EntityTeam.Red ? EntityTeam.Blue : EntityTeam.Red;
        }
    }
}
