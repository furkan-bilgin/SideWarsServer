using Ara3D;
using SideWars.Shared.Game;
using SideWars.Shared.Packets;
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
using System.Diagnostics;

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
        public int CurrentRound { get; private set; }
        public float CurrentRoundTime { get; private set; }

        public Dictionary<EntityTeam, int> Scoreboard { get; private set; }

        private List<IEntityUpdater> entityUpdaters;
        private List<IGameLoop> gameLoops;

        private int currentEntityId;

        public BaseGameRoom()
        {
            PacketSender = new BaseGameRoomPacketSender(this);
            InitGameTickers();

            ProjectileSpawner = new ProjectileSpawner();
            Players = new Dictionary<int, PlayerConnection>();
            Entities = new Dictionary<int, Entity>();
            Listener = new BaseGameRoomListener(this);
            Scoreboard = new Dictionary<EntityTeam, int>() { { EntityTeam.Blue, 0 }, { EntityTeam.Red, 0 } };
            newRoundTimer = new Stopwatch();
            Server.Instance.LogicController.RegisterLogicUpdate(Update);

            var team = GetNextTeam();
            AddPlayer(new MockPlayerConnection(ChampionType.Galacticus));
        }

        private void InitGameTickers()
        {
            RoomScheduler = new RoomScheduler();

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
                new RoundGameLoop(),
                new PacketSenderGameLoop()
            };
        }

        ~BaseGameRoom()
        {
            Dispose();
        }

        public void Dispose()
        {
            Server.Instance.LogicController.UnregisterLogicUpdate(Update);
            GC.SuppressFinalize(this);
        }

        public void AddPlayer(PlayerConnection playerConnection)
        {
            playerConnection.CurrentGameRoom = this;
            PacketSender.SendAllEntitySpawns(playerConnection);

            // Re-assign new PlayerConnection to the old player
            if (Players.ContainsKey(playerConnection.Token.ID))
            {
                var oldPlayer = this.GetPlayer(playerConnection.Token.ID);
                if (oldPlayer != null)
                    oldPlayer.PlayerConnection = playerConnection;

                Players[playerConnection.Token.ID] = playerConnection;
                return;
            }

            Players[playerConnection.Token.ID] = playerConnection;
            SpawnPlayerEntity(playerConnection);

            Logger.Info("Added player " + playerConnection.Token + " to the room");
        }

        public Entity SpawnPlayerEntity(PlayerConnection playerConnection)
        {
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
                    throw new Exception("Unknown champion.");
            }

            return SpawnEntity(player);
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

        public void StartGame()
        {
            if (RoomState != GameRoomState.Waiting)
                throw new System.Exception("Game is already started");

            RoomState = GameRoomState.RoundUpdate;
            Logger.Info("Starting game...");
        }

        public void FinishGame(EntityTeam winnerTeam)
        {
            RoomState = GameRoomState.Closed;
            PacketSender.SendRoundUpdatePacket(CurrentRound, winnerTeam, true);

            // TODO: Do post-game API update and stuff
        }

        protected virtual void Update()
        {
            if (RoomState == GameRoomState.RoundUpdate)
            {
                NewRoundUpdate();
                return;
            }

            if (RoomState != GameRoomState.Started)
                return;

            lock (Entities) lock (Players)
            {
                Tick++;
                CurrentRoundTime -= LogicTimer.FixedDelta;

                UpdateEntityUpdaters();
                UpdateGameLoops();
            }
        }

        private Stopwatch newRoundTimer;
        private bool newRoundSpawnedPlayers;
        protected virtual void NewRoundUpdate()
        {
            // If we haven't started timer
            if (!newRoundTimer.IsRunning)
            {
                newRoundTimer.Start();

                var lastWinner = EntityTeam.None;
                CurrentRound++;

                if (CurrentRound > 1)
                {
                    var aliveTeam = this.GetAliveTeams().FirstOrDefault();

                    // If both teams are completely dead, select a random winner
                    if (aliveTeam == EntityTeam.None)
                        aliveTeam = RandomTool.Current.Float(0, 1) >= 0.5f ? EntityTeam.Red : EntityTeam.Blue;

                    lastWinner = aliveTeam;
                    Scoreboard[aliveTeam] += 1;

                    // Check scoreboard
                    foreach (var item in Scoreboard)
                    {
                        // Send game-over packet if this team has won
                        if (item.Value >= RoomOptions.MaxScore)
                        {
                            FinishGame(aliveTeam);
                            return;
                        }
                    }

                    newRoundSpawnedPlayers = false;
                } 
                else
                {
                    newRoundSpawnedPlayers = true;
                }

                PacketSender.SendRoundUpdatePacket(CurrentRound, lastWinner, false);
            }

            // TODO: change magic values
            if (newRoundTimer.Elapsed.Seconds >= 3)
            {
                if (!newRoundSpawnedPlayers)
                {
                    // Kill all entities and spawn players
                    foreach (var entity in Entities)
                        entity.Value.Kill();

                    foreach (var item in Players)
                        SpawnPlayerEntity(item.Value);

                    InitGameTickers();
                    UpdateGameLoops();
                    newRoundSpawnedPlayers = true;
                }
            } 

            if (newRoundTimer.Elapsed.Seconds >= 6)
            {
                // Start the game
                newRoundTimer.Reset();
                CurrentRoundTime = RoomOptions.RoundTime;
                RoomState = GameRoomState.Started;
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
