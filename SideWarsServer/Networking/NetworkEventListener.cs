using LiteNetLib;
using SideWarsServer.Utils;
using System.Net;
using System.Net.Sockets;

namespace SideWarsServer.Networking
{
    public class NetworkEventListener : INetEventListener
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

                Server.Instance.RoomController.JoinOrCreateRoom(playerConnection);
            }
            else
                request.Reject();
        }

        public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
        {

        }

        public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
        {

        }

        public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {

        }

        public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
        {

        }

        public void OnPeerConnected(NetPeer peer)
        {
            Logger.Info("Peer connected with id " + peer.Id);
        }

        public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            Logger.Info("Peer disconnected with id " + peer.Id);
        }
    }
}
