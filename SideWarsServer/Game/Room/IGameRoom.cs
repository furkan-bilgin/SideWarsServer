using SideWarsServer.Game.Logic;
using SideWarsServer.Game.Room.Listener;
using SideWarsServer.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideWarsServer.Game.Room
{
    public interface IGameRoom
    {
        RoomOptions RoomOptions { get; }
        GameRoomState RoomState { get; set; }
        IGameRoomListener Listener { get; set; }
        Dictionary<int, Entity> Entities { get; set; }
        int Tick { get; }

        void AddPlayer(PlayerConnection playerConnection);
        void RemovePlayer(PlayerConnection playerConnection);
    }
}
