using LiteNetLib;
using LiteNetLib.Utils;
using SideWars.Shared.Packets;
using SideWarsServer.Game.Room;
using SideWarsServer.Utils;
using System;

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

        private async void ReadyPacketReceive(ReadyPacket packet, NetPeer netPeer)
        {
            await Wait.While(() => !Server.Instance.PlayerController.Players.ContainsKey(netPeer.Id));

            var player = Server.Instance.PlayerController.Players[netPeer.Id];
            if (player.CurrentGameRoom != null && player.CurrentGameRoom.RoomState == GameRoomState.Waiting)
            {
                player.CurrentGameRoom.Listener.OnPlayerReady(player);
            }
        }

        private void PlayerMovementPacketRecieve(PlayerMovementPacket packet, NetPeer netPeer)
        {
            try
            {
                var player = Server.Instance.PlayerController.Players[netPeer.Id];
                if (player.CurrentGameRoom != null && player.CurrentGameRoom.RoomState != GameRoomState.Waiting)
                {
                    player.CurrentGameRoom.Listener.OnPlayerMovementChange(player, packet.Horizontal, packet.Jump);
                }
            } 
            catch(Exception ex)
            {
                Logger.Error(ex.ToString());
            }
        }

    }
}
