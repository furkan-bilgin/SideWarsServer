using Ara3D;
using SideWarsServer.Networking;

namespace SideWarsServer.Game.Room.Listener
{
    public interface IGameRoomListener
    {
        void OnPlayerReady(PlayerConnection player);
        void OnPlayerLocationChange(PlayerConnection player, Vector3 location);
    }
}
