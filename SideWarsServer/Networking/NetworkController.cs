using LiteNetLib;
using LiteNetLib.Utils;
using SideWarsServer.Utils;

namespace SideWarsServer.Networking
{
    public class NetworkController
    {
        public NetPacketProcessor PacketProcessor { get; private set; }
        private NetManager client;
        private NetworkEventListener networkEventListener;

        public NetworkController()
        {
            PacketProcessor = new NetPacketProcessor();
        }

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
