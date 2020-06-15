using SideWarsServer.Game.Logic;
using SideWarsServer.Game.Room.Listener;
using SideWarsServer.Networking;
using SideWarsServer.Utils;
using System.Collections.Generic;

namespace SideWarsServer.Game.Room
{
    public class BaseGameRoom : IGameRoom
    {
        public IGameRoomListener Listener { get; set; }
        protected Dictionary<int, Player> players;

        public BaseGameRoom()
        {
            players = new Dictionary<int, Player>();
            Listener = new BaseGameRoomListener(this);

            Server.Instance.LogicController.RegisterLogicUpdate(Update);
        }

        ~BaseGameRoom()
        {
            Server.Instance.LogicController.UnregisterLogicUpdate(Update);
        }

        public void AddPlayer(PlayerConnection playerConnection)
        {
            Logger.Info("Added player " + playerConnection.NetPeer.Id + " to the room");

            playerConnection.CurrentGameRoom = this;
            players.Add(playerConnection.NetPeer.Id, new Player(Ara3D.Vector3.Zero, playerConnection));
        }

        public void RemovePlayer(PlayerConnection playerConnection)
        {
            players.Remove(playerConnection.NetPeer.Id);
        }

        protected virtual void Update()
        {

        }
    }
}
