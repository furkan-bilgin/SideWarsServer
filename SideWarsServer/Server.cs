using SideWarsServer.Networking;
using SideWarsServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideWarsServer
{
    public class Server
    {
        public bool shutdown;
        public NetworkManager networkManager;

        public async Task StartServerThread()
        {
            Logger.Info("Starting server thread...");

            networkManager = new NetworkManager();
            networkManager.StartServer();

            while (!shutdown)
            {
                networkManager.Update();
                await Task.Delay(50);
            }
        }
    }
}
