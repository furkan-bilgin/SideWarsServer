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
            server.BroadcastReceiveEnabled = true;
            
            server.Start(443);
            
            Logger.Info("Started listening at 443");
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
