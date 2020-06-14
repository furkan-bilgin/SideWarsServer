using SideWarsServer.Game;
using SideWarsServer.Networking;
using SideWarsServer.Threading;
using SideWarsServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideWarsServer
{
    public class Server : Singleton<Server>
    {
        public bool Shutdown { get; set; }
        public NetworkManager NetworkManager { get; set; }
        public LogicController LogicController { get; set; }
        public TaskController TaskController { get; set; }

        public Server()
        {
            base.InitSingleton(this);
        }

        public async Task StartServerThread()
        {
            Logger.Info("Starting server thread...");

            var threadCount = Environment.ProcessorCount;

            NetworkManager = new NetworkManager();
            
            LogicController = new LogicController(threadCount);
            TaskController = new TaskController(threadCount);

            NetworkManager.StartServer();

            while (!Shutdown)
            {
                NetworkManager.Update();
                await Task.Delay(50);
            }

            Environment.Exit(0);
        }
    }
}
