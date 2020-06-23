using Ara3D;
using LiteNetLib;
using SideWars.Shared.Packets;
using SideWars.Shared.Physics;
using SideWarsServer.Game.Logic;
using SideWarsServer.Game.Logic.Models;
using SideWarsServer.Game.Room.Listener;
using SideWarsServer.Networking;
using SideWarsServer.Utils;
using System.Buffers;
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

        private CollisionController collisionController;
        private ProjectileSpawner projectileSpawner;
        private Dictionary<PlayerConnection, Player> playerEntities;
        private List<Entity> deadEntities;
        private int currentEntityId;
        private int tickCount;

        public BaseGameRoom()
        {
            deadEntities = new List<Entity>();
            collisionController = new CollisionController();
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

            var team = GetNextTeam();
            var player = new Player(RoomOptions.GetSpawnPoint(team), playerConnection, team);

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

            var addX = playerConnection.Latency / 1000 * playerMovement.Speed * horizontal * LogicTimer.FixedDelta; // A little lag compensation but it probably makes things worse :P
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
            entity.BirthTick = tickCount;

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

            lock (Entities) lock (Players)
            {
                tickCount++;
                deadEntities.Clear();

                UpdateEntityMovements();
                UpdateColliders();
                UpdateCollisions();
                UpdateEntities();

                CheckEntityHealths();

                SendEntityDeathPackets();
                SendPlayerMovementPackets();
                SendMovementPackets();
            }
        }

        protected void OnEntityCollision(Entity entity, Entity collidingEntity)
        {
            if (entity is Bullet)
            {
                Logger.Info(entity.Type + " collided " + collidingEntity.Type);
                if (entity.Team == collidingEntity.Team)
                    return;

                var bullet = (Bullet)entity;

                if (collidingEntity is Player)
                {
                    collidingEntity.Hurt(bullet.ProjectileInfo.Damage);
                    entity.Kill();
                }
            }
        }

        private void UpdateEntities()
        {
            foreach (var item in Entities)
            {
                if (item.Value is ITimedDestroy)
                {
                    var timedDestroy = (ITimedDestroy)item.Value;
                    if (tickCount - item.Value.BirthTick >= LogicTimer.FramesPerSecond * timedDestroy.DestroySeconds) // If the time has passed
                    {
                        item.Value.Kill(); // Kill the entity
                    }
                }
            }
        }

        private void UpdateEntityMovements()
        {
            foreach (var item in Entities)
            {
                var entity = item.Value;
                var location = entity.Location;
                entity.Movement.Update(LogicTimer.FixedDelta, ref location);

                entity.Location = location;
            }
        }

        private void UpdateColliders()
        {
            foreach (var item in Entities)
            {
                item.Value.Collider.UpdateLocation(item.Value.Location);
            }
        }

        private void UpdateCollisions()
        {
            var entityList = Entities.Select((x) => x.Value).ToList();
            collisionController.GetCollidingEntities(entityList, OnEntityCollision);
        }

        private void CheckEntityHealths()
        {
            foreach (var item in Entities)
            {
                var entity = item.Value;
                if (entity.Health <= 0)
                {
                    deadEntities.Add(entity);
                }
            }

            foreach (var item in deadEntities)
            {
                Entities.Remove(item.Id);
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
