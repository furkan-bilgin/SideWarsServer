using SideWarsServer.Game.Logic;
using SideWarsServer.Game.Room.Listener;
using SideWarsServer.Networking;
using SideWarsServer.Utils;
using System.Collections.Generic;

namespace SideWarsServer.Game.Room
{
    public class BaseGameRoom : IGameRoom
    {
        public GameRoomState RoomState { get; set; }
        public IGameRoomListener Listener { get; set; }
        public Dictionary<int, Player> Players { get; set; }

        public BaseGameRoom()
        {
            Players = new Dictionary<int, Player>();
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
            Players.Add(playerConnection.NetPeer.Id, new Player(Ara3D.Vector3.Zero, playerConnection));
        }

        public void RemovePlayer(PlayerConnection playerConnection)
        {
            Players.Remove(playerConnection.NetPeer.Id);
        }

        public void StartGame()
        {
            if (RoomState != GameRoomState.Waiting)
                throw new System.Exception("Game is already started");

            Logger.Info("Start game");
            // TODO: Game start
        }

        protected virtual void Update()
        {

        }
    }
}
