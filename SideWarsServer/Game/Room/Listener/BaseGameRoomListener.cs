using Ara3D;
using SideWars.Shared.Packets;
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

        public void OnPlayerMovementChange(PlayerConnection player, float horizontal, bool jump, PlayerButton[] buttons)
        {
            gameRoom.UpdatePlayerMovement(player, horizontal, jump, buttons);
        }

        public void OnPlayerReady(PlayerConnection player)
        {
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
