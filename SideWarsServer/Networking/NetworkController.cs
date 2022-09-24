using LiteNetLib;
using LiteNetLib.Utils;
using SideWarsServer.Utils;

namespace SideWarsServer.Networking
{
    public class NetworkController
    {
        public NetPacketProcessor PacketProcessor { get; private set; }

        private NetManager server;
        private NetworkEventListener networkEventListener;

        public NetworkController()
        {
            PacketProcessor = new NetPacketProcessor();
        }

        public void StartServer()
        {
            networkEventListener = new NetworkEventListener();
            server = new NetManager(networkEventListener);
            server.UpdateTime = 20;
            server.Start(5000);
            
            Logger.Info("Started listening at 5000");
        }

        public void Update()
        {
            server.PollEvents();
        }

        public void SendPacket<T>(NetPeer netPeer, T packet, DeliveryMethod deliveryMethod = DeliveryMethod.ReliableOrdered) where T : class, new()
        {
            PacketProcessor.Send(netPeer, packet, deliveryMethod);
        }
    }
}
