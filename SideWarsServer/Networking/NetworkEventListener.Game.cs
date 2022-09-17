using LiteNetLib;
using LiteNetLib.Utils;
using SideWars.Shared.Packets;
using SideWarsServer.Game.Room;
using SideWarsServer.Utils;
using System;
using System.Linq;

namespace SideWarsServer.Networking
{
    public partial class NetworkEventListener
    {
        private NetPacketProcessor netPacketProcessor;

        public NetworkEventListener()
        {
            netPacketProcessor = Server.Instance.NetworkController.PacketProcessor;
            netPacketProcessor.SubscribeReusable<ReadyPacket, NetPeer>(ReadyPacketReceive);
            netPacketProcessor.SubscribeReusable<PlayerUpdatePacket, NetPeer>(PlayerMovementPacketRecieve);
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

        private void PlayerMovementPacketRecieve(PlayerUpdatePacket packet, NetPeer netPeer)
        {
            try
            {
                var player = Server.Instance.PlayerController.Players[netPeer.Id];
                var buttons = packet.PlayerButtons.Select((x) => (PlayerButton)x).ToArray();

                if (player.CurrentGameRoom != null && player.CurrentGameRoom.RoomState != GameRoomState.Waiting)
                {
                    player.CurrentGameRoom.Listener.OnPlayerMovementChange(player, packet.Horizontal, buttons);
                }
            } 
            catch(Exception ex)
            {
                Logger.Error(ex.ToString());
            }
        }

    }
}
