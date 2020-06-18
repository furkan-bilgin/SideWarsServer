using Ara3D;
using SideWarsServer.Networking;
using SideWarsServer.Utils;

namespace SideWarsServer.Game.Room.Listener
{
    public class BaseGameRoomListener : IGameRoomListener
    {
        private BaseGameRoom gameRoom;

        public BaseGameRoomListener(BaseGameRoom room)
        {
            gameRoom = room;
        }

        public void OnPlayerMovementChange(PlayerConnection player, float horizontal, bool jump)
        {
            gameRoom.UpdatePlayerMovement(player, horizontal, jump);
        }

        public void OnPlayerReady(PlayerConnection player)
        {
            Logger.Info("Player ready");
            gameRoom.Players[player.NetPeer.Id].IsReady = true;

            if (gameRoom.Players.Count < gameRoom.RoomOptions.MaxPlayers)
                return;

            foreach (var item in gameRoom.Players)
                if (!item.Value.IsReady)
                    return;

            gameRoom.StartGame();
        }
    }
}
