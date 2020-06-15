using Ara3D;
using SideWarsServer.Networking;

namespace SideWarsServer.Game.Room.Listener
{
    public class BaseGameRoomListener : IGameRoomListener
    {
        private BaseGameRoom gameRoom;

        public BaseGameRoomListener(BaseGameRoom room)
        {
            gameRoom = room;
        }

        public void OnPlayerLocationChange(PlayerConnection player, Vector3 location)
        {
            gameRoom.Players[player.NetPeer.Id].Location = location;
        }

        public void OnPlayerReady(PlayerConnection player)
        {
            gameRoom.Players[player.NetPeer.Id].IsReady = true;

            foreach (var item in gameRoom.Players)
                if (!item.Value.IsReady)
                    return;

            gameRoom.StartGame();
        }
    }
}
