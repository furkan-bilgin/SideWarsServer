using SideWarsServer.Game.Logic;
using SideWarsServer.Networking;
using System.Collections.Generic;

namespace SideWarsServer.Game.Room
{
    public class BaseGameRoom : IGameRoom
    {
        protected Dictionary<int, Player> players;

        public BaseGameRoom()
        {
            players = new Dictionary<int, Player>();
            Server.Instance.LogicController.RegisterLogicUpdate(Update);
        }

        ~BaseGameRoom()
        {
            Server.Instance.LogicController.UnregisterLogicUpdate(Update);
        }

        public void AddPlayer(PlayerConnection playerConnection)
        {
            players.Add(playerConnection.NetPeer.Id, new Player(Ara3D.Vector3.Zero, playerConnection));
        }

        public void RemovePlayer(PlayerConnection playerConnection)
        {
            players.Remove(playerConnection.NetPeer.Id);
        }

        protected virtual void Update()
        {
            // TODO: Game logic
        }
    }
}
