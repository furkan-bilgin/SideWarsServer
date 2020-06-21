using Ara3D;
using LiteNetLib;
using SideWars.Shared.Packets;
using SideWars.Shared.Physics;
using SideWarsServer.Game.Logic;
using SideWarsServer.Game.Room.Listener;
using SideWarsServer.Networking;
using SideWarsServer.Utils;
using System.Collections.Generic;
using System.Linq;

namespace SideWarsServer.Game.Room
{
    public partial class BaseGameRoom : IGameRoom
    {
        public RoomOptions RoomOptions { get => RoomOptions.Default; }
        public GameRoomState RoomState { get; set; }
        public IGameRoomListener Listener { get; set; }
        public Dictionary<int, Entity> Entities { get; set; }
        public Dictionary<int, PlayerConnection> Players { get; set; }

        private ProjectileSpawner projectileSpawner;
        private Dictionary<PlayerConnection, Player> playerEntities;
        private int currentEntityId;
        private long tickCount;

        public BaseGameRoom()
        {
            projectileSpawner = new ProjectileSpawner();
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

            var player = new Player(Vector3.Zero, playerConnection);
            player.Team = GetNextTeam();
            player.Location = RoomOptions.GetSpawnPoint(player.Team);

            playerEntities.Add(playerConnection, player);

            SpawnEntity(player);
        }

        public void RemovePlayer(PlayerConnection playerConnection)
        {
            Entities.Remove(playerConnection.NetPeer.Id);
            playerEntities.Remove(playerConnection);

            if (Entities.Count == 0)
            {
                RoomState = GameRoomState.Closed;
            }
        }

        public void UpdatePlayerMovement(PlayerConnection playerConnection, float horizontal, bool jump, PlayerButton[] buttons)
        {
            var player = playerEntities[playerConnection];
            var playerMovement = (PlayerMovement)playerEntities[playerConnection].Movement;

            var addX = playerConnection.Latency / 1000 * playerMovement.Speed * horizontal * LogicTimer.FixedDelta;

            player.Location = player.Location.SetX(player.Location.X + addX);

            playerMovement.Horizontal = horizontal;
            playerMovement.Jump = jump;

            if (buttons.Contains(PlayerButton.Fire))
            {
                if (player.PlayerCombat.Shoot())
                {
                    var projectile = projectileSpawner.SpawnProjectile(player.PlayerInfo.ProjectileType, player);
                    SpawnEntity(projectile);
                }
            }
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

            UpdateEntityMovements();
            UpdateColliders();
            SendPlayerMovementPackets();
            SendMovementPackets();
        }

        void UpdateEntityMovements()
        {
            foreach (var item in Entities)
            {
                var entity = item.Value;
                var location = entity.Location;
                entity.Movement.Update(LogicTimer.FixedDelta, ref location);

                entity.Location = location;
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
                        if (tickCount % LogicTimer.FramesPerSecond == 0) // Send Player positions every second in case of sync issues. 
                            sendEntityMovement();
                    }
                    /*
                    else
                    {
                        sendEntityMovement();
                    }*/
                }

            }
        }

        void SendPlayerMovementPackets()
        {
            foreach (var playerItem in playerEntities)
            {
                foreach (var item in Players)
                {
                    if (item.Value.NetPeer != playerItem.Key.NetPeer)
                    {
                        SendPlayerMovement(playerItem.Value, item.Value.NetPeer);
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

        EntityTeam team = EntityTeam.Red;
        EntityTeam GetNextTeam()
        {
            team = team == EntityTeam.Red ? EntityTeam.Blue : EntityTeam.Red;
            return team;
        }
    }
}
