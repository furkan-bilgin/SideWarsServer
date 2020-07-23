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

        public int Tick { get; private set; }

        private CollisionController collisionController;
        private Dictionary<Player, PlayerConnection> playerEntities;
        private List<IEntityUpdater> entityUpdaters;
        private List<IGameLoop> gameLoops;

        private List<(Player, SpellInfo)> spellUses;
        
        private int currentEntityId;
        private List<Entity> tempEntityList;

        public BaseGameRoom()
        {
            spellUses = new List<(Player, SpellInfo)>();

            collisionController = new CollisionController();
            playerEntities = new Dictionary<Player, PlayerConnection>();
            entityUpdaters = new List<IEntityUpdater>()
            {
                new GrenadeUpdater(),
                new TimedDestroyUpdater(),
                new StatusEffectUpdater()
            };

            gameLoops = new List<IGameLoop>()
            {
                new EntityMovementGameLoop(),
                new CollisionGameLoop(OnEntityCollision),

                new ActionGameLoop(() => RoomScheduler.Update(Tick)),

                new EntityHealthGameLoop(SendEntityHealthChangePackets),
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
            Server.Instance.LogicController.UnregisterLogicUpdate(Update);
        }

        public void AddPlayer(PlayerConnection playerConnection)
        {
            Logger.Info("Added player " + playerConnection.NetPeer.Id + " to the room");
            playerConnection.CurrentGameRoom = this;

            SendAllEntitySpawns(playerConnection.NetPeer);

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

                default:
                    throw new System.Exception("Unknown champion.");
            }

            Players.Add(playerConnection.NetPeer.Id, playerConnection);
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

        public void UpdatePlayerMovement(PlayerConnection playerConnection, float horizontal, PlayerButton[] buttons)
        {
            var player = playerEntities[playerConnection];
            var playerMovement = (PlayerMovement)playerEntities[playerConnection].Movement;

            //var addX = playerConnection.Latency / 1000 * playerMovement.Speed * horizontal * LogicTimer.FixedDelta; // A little lag compensation but it probably makes things worse :P
            //player.Location = player.Location.SetX(player.Location.X + addX);

            playerMovement.Horizontal = horizontal;

            foreach (var button in buttons)
            {
                if (button == PlayerButton.Special1 || button == PlayerButton.Special2)
                {
                    var spell = player.PlayerSpells.SpellInfo.GetSpellInfo(button);
                    if (player.PlayerSpells.Cast(this, player, spell))
                    {
                        spellUses.Add((player, spell));
                    }
                }
                else if (button == PlayerButton.Fire)
                {
                    if (player.PlayerCombat.Shoot())
                    {
                        new PlayerShootEffect(player).Start(this);
                    }
                }
            }
            
        }

        public Entity SpawnEntity(Entity entity)
        {
            var id = ++currentEntityId;
            entity.Id = id;
            entity.BirthTick = Tick;

            Entities.Add(id, entity); // Add entity to Entities dictionary

            foreach (var item in Players)
            {
                var peer = item.Value.NetPeer;
                SendEntitySpawn(entity, peer); // Send everyone EntitySpawnPacket
            }

            return entity;
        }

        public void SpawnParticle(ParticleType particleType, Vector3 location, float[] data = null)
        {
            if (data == null)
                data = new float[0];

            foreach (var item in Players)
            {
                SendParticleSpawn(particleType, location, data, item.Value.NetPeer);
            }
        }

        public List<Entity> GetEntities()
        {
            return tempEntityList;
        }


        public PlayerConnection GetPlayerConnection(Player player)
        {
            return playerEntities[player]
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

            lock (Entities)
            {
                lock (Players)
                {
                    Tick++;

                    tempEntityList = Entities.Values.ToList();

                    UpdateGameLoops();


                    UpdateEntityUpdaters();

                    SendEntityDeathPackets();
                    SendPlayerMovementPackets();
                    SendMovementPackets();
                    SendPlayerSpellUsePackets();

                    tempEntityList.Clear();
                }
            }
        }

        protected void OnEntityCollision(Entity entity, Entity collidingEntity)
        {
            Logger.Info(entity.ToString());
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
