using SideWarsServer.Networking;
using System;
using System.Collections.Generic;

namespace SideWarsServer.Game.Room
{
    public class RoomController
    {
        public Dictionary<string, IGameRoom> GameRooms { get; private set; }

        public RoomController()
        {
            GameRooms = new Dictionary<string, IGameRoom>();
        }

        public bool JoinOrCreateRoom(PlayerConnection playerConnection)
        {
            try
            {
                if (!GameRooms.ContainsKey(playerConnection.Token.RoomId)) // If the room doesn't exist
                {
                    var gameRoom = new BaseGameRoom();
                    gameRoom.AddPlayer(playerConnection);

                    GameRooms.Add(playerConnection.Token.RoomId, gameRoom);
                }
                else // If it is, add it
                {
                    var gameRoom = GameRooms[playerConnection.Token.RoomId];
                    gameRoom.AddPlayer(playerConnection);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
