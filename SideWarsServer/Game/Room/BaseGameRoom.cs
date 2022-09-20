using Ara3D;
using SideWars.Shared.Game;
using SideWars.Shared.Packets;
using SideWars.Shared.Physics;
using SideWarsServer.Game.Logic;
using SideWarsServer.Game.Logic.Champions;
using SideWarsServer.Game.Logic.Effects;
using SideWarsServer.Game.Logic.Projectiles;
using SideWarsServer.Game.Logic.Updater;
using SideWarsServer.Game.Room.Listener;
using SideWarsServer.Networking;
using SideWarsServer.Utils;
using System.Collections.Generic;
using System.Linq;
using SideWarsServer.Game.Logic.Scheduler;
using SideWarsServer.Game.Logic.GameLoop;
using System;
using System.Threading.Tasks;

namespace SideWarsServer.Game.Room
{
    public partial class BaseGameRoom : IGameRoom
    {
        public RoomScheduler RoomScheduler { get; set; }
        public RoomOptions RoomOptions { get => RoomOptions.Default; }
        public GameRoomState RoomState { get; set; }
        public IGameRoomListener Listener { get; set; }
        public Dictionary<int, Entity> Entities { get; set; }
        public Dictionary<int, PlayerConnection> Players { get; set; }
        public ProjectileSpawner ProjectileSpawner { get; set; }
        public BaseGameRoomPacketSender PacketSender { get; set; }

        public int Tick { get; private set; }

        private CollisionController collisionController;
        private List<IEntityUpdater> entityUpdaters;
        private List<IGameLoop> gameLoops;

        private int currentEntityId;

        public BaseGameRoom()
        {
            PacketSender = new BaseGameRoomPacketSender(this);

            collisionController = new CollisionController();
            entityUpdaters = new List<IEntityUpdater>()
            {
                new GrenadeUpdater(),
                new TimedDestroyUpdater(),
                new StatusEffectUpdater(),
                new CustomStatusEffectUpdater()
            };

            gameLoops = new List<IGameLoop>()
            {
                new PlayerMovementGameLoop(),
                new EntityMovementGameLoop(),
                new CollisionGameLoop(OnEntityCollision),
                new ActionGameLoop(() => RoomScheduler.Update(Tick)),
                new EntityHealthGameLoop(),
                new PacketSenderGameLoop()
            };

            ProjectileSpawner = new ProjectileSpawner();
            Players = new Dictionary<int, PlayerConnection>();
            Entities = new Dictionary<int, Entity>();
            Listener = new BaseGameRoomListener(this);
            RoomScheduler = new RoomScheduler();

            Server.Instance.LogicController.RegisterLogicUpdate(Update);
        }

        ~BaseGameRoom()
        {
            Logger.Info("GameRoom dispose");
            Server.Instance.LogicController.UnregisterLogicUpdate(Update);
        }

        public void AddPlayer(PlayerConnection playerConnection)
        {
            Logger.Info("Added player " + playerConnection.NetPeer.Id + " to the room");
            playerConnection.CurrentGameRoom = this;

            PacketSender.SendAllEntitySpawns(playerConnection);

            if (Players.ContainsKey(playerConnection.Token.ID))
            {
                var oldPlayer = this.GetPlayer(playerConnection.Token.ID);
                oldPlayer.PlayerConnection = playerConnection;
                Players[playerConnection.Token.ID] = playerConnection;

                return;
            }

            var team = GetNextTeam();
            var spawnPoint = RoomOptions.GetSpawnPoint(team);
            Player player;
            
            switch (playerConnection.Token.ChampionType)
            {
                case ChampionType.Mark:
                    player = new Mark(spawnPoint, playerConnection, team);
                    break;
                case ChampionType.Hyrex:
                    player = new Hyrex(spawnPoint, playerConnection, team);
                    break;
                case ChampionType.Desgama:
                    player = new Desgama(spawnPoint, playerConnection, team);
                    break;
                case ChampionType.Galacticus:
                    player = new Galacticus(spawnPoint, playerConnection, team);
                    break;
                default:
                    throw new System.Exception("Unknown champion.");
            }

            Players[playerConnection.Token.ID] = playerConnection;
            SpawnEntity(player);
        }

        public void RemovePlayer(PlayerConnection playerConnection)
        {
            Entities.Remove(playerConnection.NetPeer.Id);
        }

        public void UpdatePlayerMovement(PlayerConnection playerConnection, float horizontal, PlayerButton[] buttons)
        {
            if (RoomState == GameRoomState.Started)
                GetGameLoop<PlayerMovementGameLoop>().AddBuffer(playerConnection, horizontal, buttons);
        }

        public Entity SpawnEntity(Entity entity)
        {
            var id = ++currentEntityId;
            entity.Id = id;
            entity.BirthTick = Tick;

            Entities.Add(id, entity); // Add entity to Entities dictionary

            foreach (var item in Players)
            {
                PacketSender.SendEntitySpawn(entity, item.Value); // Send everyone EntitySpawnPacket
            }

            return entity;
        }

        public void SpawnParticle(ParticleType particleType, Vector3 location, float[] data = null)
        {
            if (data == null)
                data = new float[0];

            foreach (var item in Players)
            {
                PacketSender.SendParticleSpawn(particleType, location, data, item.Value);
            }
        }

        public List<Entity> GetEntities()
        {
            return Entities.Values.ToList(); // Might change later idk.
        }

        public T GetGameLoop<T>() where T : IGameLoop
        {
            return (T)gameLoops.Where(x => x is T).First();
        }

        public async void StartGame()
        {
            if (RoomState != GameRoomState.Waiting)
                throw new System.Exception("Game is already started");

            RoomState = GameRoomState.Countdown;

            // Send countdown packet and wait 3 seconds.
            PacketSender.SendCountdownPacket();
            
            // TODO: UNCOMMENT THIS await Task.Delay(3000);
            
            // Then start the game.
            RoomState = GameRoomState.Started;

            Logger.Info("Game started");
        }

        protected virtual void Update()
        {
            if (RoomState != GameRoomState.Started)
                return;

            lock (Entities) lock (Players)
            {
                Tick++;

                UpdateEntityUpdaters();
                UpdateGameLoops();
            }
        }

        protected void OnEntityCollision(Entity entity, Entity collidingEntity)
        {
            if (entity is Bullet)
            {
                new BulletCollisionEffect((Bullet)entity, collidingEntity).Start(this);
            }
        }

        #region Tick Functions

        private void UpdateGameLoops()
        {
            foreach (var loop in gameLoops)
            {
                loop.Update(this);
            }
        }

        private void UpdateEntityUpdaters()
        {
            foreach (var item in entityUpdaters)
            {
                foreach (var entity in Entities)
                {
                    item.Update(entity.Value, this);
                }
            }
        }

        EntityTeam team = EntityTeam.Red;
        EntityTeam GetNextTeam()
        {
            team = team == EntityTeam.Red ? EntityTeam.Blue : EntityTeam.Red;
            return team;
        }
        #endregion
    }
}
