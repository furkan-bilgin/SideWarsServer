using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideWarsServer.Utils
{
    public static class Wait
    {
        public static async Task While(Func<bool> statement, int delay = 250)
        {
            while (statement())
            {
                await Task.Delay(delay);
            }
        }
    }
}
