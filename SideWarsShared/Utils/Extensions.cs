using SideWars.Shared.Packets;
using System;
using System.Collections.Generic;
using System.Text;

namespace SideWars.Shared.Utils
{
    public static class Extensions
    {
        public static int ToMultiplier(this EntityTeam team)
        {
            return team == EntityTeam.Blue ? 1 : -1;
        }
    }
}
