using LiteNetLib;
using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SideWarsServer
{
    class Program
    {
        static void Main(string[] args)
        {
            new Server().StartServerThread().Wait();
        }
    }
}
