using LiteNetLib;
using LiteNetLib.Utils;
using SideWarsServer.Utils;
using System.Net;
using System.Net.Sockets;

namespace SideWarsServer.Networking
{
    public partial class NetworkEventListener : INetEventListener
    {
        public async void OnConnectionRequest(ConnectionRequest request)
        {
            string tokenId;
            if (!request.Data.TryGetString(out tokenId))
                request.Reject();

            var token = await Server.Instance.DatabaseController.GetTokenAsync(tokenId);

            if (token != null)
            {
                var peer = request.Accept();
                var playerConnection = new PlayerConnection(token, peer);

                Server.Instance.PlayerController.AddPlayer(playerConnection);
                Server.Instance.RoomController.JoinOrCreateRoom(playerConnection);
            }
            else
                request.Reject();
        }

        public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            netPacketProcessor.ReadAllPackets(reader, peer);
        }

        public void OnPeerConnected(NetPeer peer)
        {
            Logger.Info("Peer connected with id " + peer.Id);
        }

        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            Logger.Info("Peer disconnected with id " + peer.Id);
            Server.Instance.PlayerController.RemovePlayer(peer.Id);
        }

        public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
        { }

        public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
        { }

        public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
        {
            Server.Instance.PlayerController.Players[peer.Id].Latency = latency;
        }
    }
}
