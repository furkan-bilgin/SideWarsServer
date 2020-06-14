using SideWarsServer.Networking;
using System.Collections.Generic;

namespace SideWarsServer.Game.Room
{
    public class RoomController
    {
        private Dictionary<string, IGameRoom> gameRooms;

        public RoomController()
        {
            gameRooms = new Dictionary<string, IGameRoom>();
        }

        public void JoinOrCreateRoom(PlayerConnection playerConnection)
        {
            if (!gameRooms.ContainsKey(playerConnection.Token.RoomId)) // If the room doesn't exist
            {
                var gameRoom = new BaseGameRoom();
                gameRoom.AddPlayer(playerConnection);

                gameRooms.Add(playerConnection.Token.RoomId, gameRoom);
            }
            else
            {
                var gameRoom = gameRooms[playerConnection.Token.RoomId];
                gameRoom.AddPlayer(playerConnection);
            }
        }
    }
}
