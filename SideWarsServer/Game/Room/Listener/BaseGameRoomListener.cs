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

        public void OnPlayerMovementChange(PlayerConnection player, float horizontal, PlayerButton[] buttons)
        {
            gameRoom.UpdatePlayerMovement(player, horizontal, buttons);
        }

        public void OnPlayerReady(PlayerConnection player)
        {
            player.IsReady = true;

            if (gameRoom.Players.Count < gameRoom.RoomOptions.MaxPlayers)
                return;

            foreach (var item in gameRoom.Players)
                if (!item.Value.IsReady)
                    return;

            gameRoom.StartGame();
        }

        public void OnPlayerDisconnect(PlayerConnection player)
        {
            if (gameRoom.RoomState != GameRoomState.Closed)
            {
                var winnerTeam = gameRoom.GetPlayer(player.Token.ID).Team.GetOppositeTeam();
                gameRoom.FinishGame(winnerTeam);
            }
        }
    }
}
