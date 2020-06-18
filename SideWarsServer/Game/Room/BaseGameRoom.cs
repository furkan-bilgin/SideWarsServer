using Ara3D;
using LiteNetLib;
using SideWars.Shared.Packets;
using SideWarsServer.Game.Logic;
using SideWarsServer.Game.Room.Listener;
using SideWarsServer.Networking;
using SideWarsServer.Utils;
using System.Collections.Generic;

namespace SideWarsServer.Game.Room
{
    public partial class BaseGameRoom : IGameRoom
    {
        public RoomOptions RoomOptions { get => RoomOptions.Default; }
        public GameRoomState RoomState { get; set; }
        public IGameRoomListener Listener { get; set; }
        public Dictionary<int, Entity> Entities { get; set; }
        public Dictionary<int, PlayerConnection> Players { get; set; }

        private Dictionary<PlayerConnection, Player> playerEntities;
        private int currentEntityId;
        private long tickCount;

        public BaseGameRoom()
        {
            playerEntities = new Dictionary<PlayerConnection, Player>();
            Players = new Dictionary<int, PlayerConnection>();
            Entities = new Dictionary<int, Entity>();
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

            Players.Add(playerConnection.NetPeer.Id, playerConnection);
            SendAllEntitySpawns(playerConnection.NetPeer);

            var entity = SpawnEntity(new Player(Vector3.Zero, playerConnection));
            playerEntities.Add(playerConnection, (Player)entity);
        }

        public void RemovePlayer(PlayerConnection playerConnection)
        {
            Entities.Remove(playerConnection.NetPeer.Id);
            playerEntities.Remove(playerConnection);
        }

        public void UpdatePlayerMovement(PlayerConnection playerConnection, float horizontal, bool jump)
        {
            playerEntities[playerConnection].PlayerMovement.Horizontal = horizontal;
            playerEntities[playerConnection].PlayerMovement.Jump = jump;
        }

        public Entity SpawnEntity(Entity entity)
        {
            var id = ++currentEntityId;
            entity.Id = id;
            Entities.Add(id, entity); // Add entity to Entities dictionary

            foreach (var item in Players)
            {
                var peer = item.Value.NetPeer;
                SendEntitySpawn(entity, peer); // Send everyone EntitySpawnPacket
            }

            return entity;
        }

        public void StartGame()
        {
            if (RoomState != GameRoomState.Waiting)
                throw new System.Exception("Game is already started");

            RoomState = GameRoomState.Started;
            Logger.Info("Start game");
            // TODO: Game start
        }

        protected virtual void Update()
        {
            if (RoomState != GameRoomState.Started)
                return;

            tickCount++;

            SendMovementPackets();
            UpdateColliders();
            UpdatePlayerMovement();
        }

        void UpdatePlayerMovement()
        {
            foreach (var item in playerEntities)
            {
                var player = item.Value;
                var location = player.Location;

                player.PlayerMovement.Update(LogicTimer.FixedDelta, ref location);
                player.Location = location;
            }
        }

        void SendAllEntitySpawns(NetPeer netPeer)
        {
            foreach (var item in Entities)
            {
                SendEntitySpawn(item.Value, netPeer);
            }
        }

        void SendMovementPackets()
        {
            foreach (var playerItem in playerEntities)
            {
                foreach (var entityItem in Entities)
                {
                    var entity = entityItem.Value;
                    void sendEntityMovement()
                    {
                        SendEntityMovement(entity, playerItem.Key.NetPeer);
                    }

                    if (entity is Player)
                    {
                        var playerEntity = (Player)entity;
                        if (playerEntity.PlayerConnection == playerItem.Key && tickCount % LogicTimer.FramesPerSecond == 0) // This makes players receive their own coordinates every second in case of desynchronization with the server. Then the players can check them and teleport back to their server coordinates if necessary. 
                            sendEntityMovement();

                        if (playerEntity.PlayerConnection != playerItem.Key) // If the player entity doesn't belong to this PlayerConnection, then send it everytime
                            sendEntityMovement();
                    }
                    else
                    {
                        sendEntityMovement();
                    }
                }

            }
        }

        void UpdateColliders()
        {
            foreach (var item in Entities)
            {
                item.Value.Collider.UpdateLocation(item.Value.Location);
            }
        }
    }
}
