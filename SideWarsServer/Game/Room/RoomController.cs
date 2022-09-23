using SideWarsServer.Networking;
using SideWarsServer.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
                Logger.Error(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// Checks and disposes closed rooms, freeing up memory.
        /// </summary>
        public void Update()
        {
            lock (GameRooms) 
            { 
                var closedRooms = GameRooms.Where((item, id) => item.Value.RoomState == GameRoomState.Closed).ToList();

                foreach (var room in closedRooms)
                {
                    Logger.Info(room.Key + " room removed.");
                    GameRooms.Remove(room.Key);
                    room.Value.Dispose();
                }
            }
        }
    }
}
