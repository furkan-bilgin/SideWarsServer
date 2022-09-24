using Ara3D;
using SideWars.Shared.Packets;
using SideWarsServer.Networking;

namespace SideWarsServer.Game.Room.Listener
{
    public interface IGameRoomListener
    {
        void OnPlayerReady(PlayerConnection player);
        void OnPlayerMovementChange(PlayerConnection player, float horizontal, PlayerButton[] buttons);
        void OnPlayerDisconnect(PlayerConnection player);
    }
}
