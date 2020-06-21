using Ara3D;
using SideWars.Shared.Packets;

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
    }
}
