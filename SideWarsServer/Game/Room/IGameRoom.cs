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
        void AddPlayer(PlayerConnection playerConnection);
        void RemovePlayer(PlayerConnection playerConnection);
    }
}
