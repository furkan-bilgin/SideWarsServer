using SideWars.Shared.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideWarsServer.Database.Models
{
    public class Token
    {
        public ChampionType ChampionType { get; set; }
        public string Username { get; private set; }
        public string RoomId { get; private set; }
        public int ID { get; private set; }

        public Token(int tokenId, string username, string roomId, ChampionType championType)
        {
            Username = username;
            RoomId = roomId;
            ChampionType = championType;
            ID = tokenId;
        }
    }
}
