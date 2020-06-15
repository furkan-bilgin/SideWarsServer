using LiteNetLib;
using SideWarsServer.Database.Models;
using SideWarsServer.Game.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideWarsServer.Networking
{
    public class PlayerConnection
    {
        public Token Token { get; private set; }
        public NetPeer NetPeer { get; private set; }
        public IGameRoom CurrentGameRoom { get; set; }
        
        public PlayerConnection(Token token, NetPeer netPeer)
        {
            Token = token;
            NetPeer = netPeer;
        }
    }
}
