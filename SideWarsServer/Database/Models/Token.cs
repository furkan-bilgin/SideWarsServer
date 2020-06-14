using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideWarsServer.Database.Models
{
    public class Token
    {
        public bool Valid { get; private set; }
        public string Username { get; private set; }
        public string RoomId { get; private set; }

        public Token(bool valid, string username, string roomId)
        {
            Valid = valid;
            Username = username;
            RoomId = roomId;
        }
    }
}
