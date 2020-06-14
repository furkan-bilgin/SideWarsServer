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
            string token;
            if (!request.Data.TryGetString(out token))
                request.Reject();

            if (await Server.Instance.RoomController.PlayerJoinRequest(token))
                request.Accept();
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
