using LiteNetLib;
using LiteNetLib.Utils;
using SideWarsServer.Game.Room;
using SideWarsServer.Networking.Shared;

namespace SideWarsServer.Networking
{
    public partial class NetworkEventListener
    {
        private NetPacketProcessor netPacketProcessor;

        public NetworkEventListener()
        {
            netPacketProcessor = Server.Instance.NetworkController.PacketProcessor;
            netPacketProcessor.SubscribeReusable<ReadyPacket, NetPeer>(ReadyPacketReceive);
        }

        private void ReadyPacketReceive(ReadyPacket packet, NetPeer netPeer)
        {
            var player = Server.Instance.PlayerController.Players[netPeer.Id];
            if (player.CurrentGameRoom != null && player.CurrentGameRoom.RoomState == GameRoomState.Waiting)
            {
                player.CurrentGameRoom.Listener.OnPlayerReady(player);
            }
        }

    }
}
