using LiteNetLib;
using LiteNetLib.Utils;
using SideWarsServer.Networking.Shared;

namespace SideWarsServer.Networking
{
    public partial class NetworkEventListener
    {
        private NetPacketProcessor netPacketProcessor;

        public NetworkEventListener()
        {
            netPacketProcessor = new NetPacketProcessor();
            netPacketProcessor.SubscribeReusable<ReadyPacket, NetPeer>(ReadyPacketReceive);
        }

        private void ReadyPacketReceive(ReadyPacket packet, NetPeer netPeer)
        {
            var player = Server.Instance.PlayerController.Players[netPeer.Id];
            if (player.CurrentGameRoom != null)
            {
                player.CurrentGameRoom.Listener.OnPlayerReady(player);
            }
        }

    }
}
