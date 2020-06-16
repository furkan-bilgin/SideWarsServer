using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideWarsServer.Networking
{
    public class PlayerController
    {
        public Dictionary<int, PlayerConnection> Players { get; set; }

        public PlayerController()
        {
            Players = new Dictionary<int, PlayerConnection>();
        }

        public void AddPlayer(PlayerConnection playerConnection)
        {
            Players.Add(playerConnection.NetPeer.Id, playerConnection);
        }

        public void RemovePlayer(PlayerConnection playerConnection)
        {
            Players.Remove(playerConnection.NetPeer.Id);
        }

        public void RemovePlayer(int id)
        {
            Players.Remove(id);
        }
    }
}
