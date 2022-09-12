using SideWarsServer.API;
using SideWarsServer.Database;
using SideWarsServer.Game;
using SideWarsServer.Game.Room;
using SideWarsServer.Networking;
using SideWarsServer.Threading;
using SideWarsServer.Utils;
using System;
using System.Threading.Tasks;

namespace SideWarsServer
{
    public class Server : Singleton<Server>
    {
        public bool Shutdown { get; set; }
        public NetworkController NetworkController { get; set; }
        public LogicController LogicController { get; set; }
        public TaskController TaskController { get; set; }
        public RoomController RoomController { get; set; }
        public DatabaseController DatabaseController { get; set; }
        public PlayerController PlayerController { get; set; }
        public APIController APIController { get; set; }

        public Server()
        {
            base.InitSingleton(this);
        }

        public async Task StartServerThread()
        {
            Logger.Info("Starting server thread...");

            var threadCount = Environment.ProcessorCount;

            NetworkController = new NetworkController();

            Logger.Info("Starting async logic with " + threadCount + " thread(s).");
            LogicController = new LogicController(threadCount);
            TaskController = new TaskController(threadCount, LogicController);
            RoomController = new RoomController();
            DatabaseController = new DatabaseController(new APITokenController());
            PlayerController = new PlayerController();
            APIController = new APIController();

            NetworkController.StartServer();

            while (!Shutdown)
            {
                NetworkController.Update();
                RoomController.Update();
                await Task.Delay(5);
            }

            Environment.Exit(0);
        }
    }
}
