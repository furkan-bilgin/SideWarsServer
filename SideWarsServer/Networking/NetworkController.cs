using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using LiteNetLib;
using SideWarsServer.Utils;

namespace SideWarsServer.Networking
{
    public class NetworkController
    {
        private NetManager client;
        private NetworkEventListener networkEventListener;

        public void StartServer()
        {
            networkEventListener = new NetworkEventListener();
            client = new NetManager(networkEventListener);
            client.Start(443);

            Logger.Info("Started listening at 443");
        }

        public void Update()
        {
            client.PollEvents();
        }
    }
}
