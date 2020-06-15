using LiteNetLib;
using LiteNetLib.Utils;
using SideWars.Shared.Packets;
using SideWarsServer.Game.Room;

namespace SideWarsServer.Networking
{
    public partial class NetworkEventListener
    {
        private NetPacketProcessor netPacketProcessor;

        public NetworkEventListener()
        {
            netPacketProcessor = Server.Instance.NetworkController.PacketProcessor;
            netPacketProcessor.SubscribeReusable<ReadyPacket, NetPeer>(ReadyPacketReceive);
            netPacketProcessor.SubscribeReusable<PlayerMovementPacket, NetPeer>(PlayerMovementPacketRecieve);
        }

        private void ReadyPacketReceive(ReadyPacket packet, NetPeer netPeer)
        {
            var player = Server.Instance.PlayerController.Players[netPeer.Id];
            if (player.CurrentGameRoom != null && player.CurrentGameRoom.RoomState == GameRoomState.Waiting)
            {
                player.CurrentGameRoom.Listener.OnPlayerReady(player);
            }
        }

        private void PlayerMovementPacketRecieve(PlayerMovementPacket packet, NetPeer netPeer)
        {
            var player = Server.Instance.PlayerController.Players[netPeer.Id];
            if (player.CurrentGameRoom != null && player.CurrentGameRoom.RoomState != GameRoomState.Waiting)
            {
                player.CurrentGameRoom.Listener.OnPlayerLocationChange(player, new Ara3D.Vector3(packet.X, packet.Y, packet.Z));
            }
        }

    }
}
