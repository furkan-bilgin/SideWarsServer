using Ara3D;
using LiteNetLib;
using SideWars.Shared.Packets;
using SideWars.Shared.Physics;
using SideWarsServer.Game.Logic;
using SideWarsServer.Game.Logic.Projectiles;
using SideWarsServer.Utils;
using SideWars.Shared.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using SideWarsServer.Networking;
using Newtonsoft.Json;

namespace SideWarsServer.Game.Room
{
    public class BaseGameRoomPacketSender
    {
        private IGameRoom gameRoom;

        public BaseGameRoomPacketSender(IGameRoom gameRoom)
        {
            this.gameRoom = gameRoom;
        }

        public void SendAllEntitySpawns(PlayerConnection connection)
        {
            foreach (var item in gameRoom.GetEntities())
            {
                SendEntitySpawn(item, connection);
            }
        }

        public void SendPlayerMovementPackets()
        {
            foreach (var item in gameRoom.Players)
            {
                SendPlayerMovement(gameRoom.GetEntities().Where(x => x is Player).Select(x => (Player)x).ToArray(), item.Value);
            }
        }

        public void SendEntityHealthChangePackets(Entity entity)
        {
            foreach (var player in gameRoom.Players)
            {
                SendEntityHealthChange(entity, player.Value);
            }
        }

        public void SendEntityDeathPackets(List<Entity> deadEntities)
        {
            foreach (var entity in deadEntities)
            {
                foreach (var player in gameRoom.Players)
                {
                    SendEntityDeath(entity, player.Value);
                }
            }
        }

        public void SendPlayerSpellUsePackets(List<(Player, SpellInfo)> spellUses)
        {
            foreach (var item in spellUses)
            {
                var (player, spell) = item;
                foreach (var roomPlayer in gameRoom.Players)
                {
                    SendPlayerSpellUse(player, spell, roomPlayer.Value);
                }
            }
        }

        public void SendEntityInfoUpdatePackets(List<(Entity, object)> entityInfoUpdates)
        {
            foreach (var item in entityInfoUpdates)
            {
                var (entity, update) = item;
                SendEntityInfoUpdate(entity, JsonConvert.SerializeObject(update));
            }
        }

        ///////// PACKET SENDER FUNCTIONS /////////

        public void SendEntitySpawn(Entity entity, PlayerConnection connection)
        {
            var data = new List<ushort>();
            var bigData = new List<float>();

            entity.Packetify(ref data, ref bigData, connection);

            connection.SendPacket(new EntitySpawnPacket()
            {
                Id = entity.Id,
                EntityType = (byte)entity.Type,
                Data = data.ToArray(),
                BigData = bigData.ToArray(),
                Team = (byte)entity.Team,
                X = entity.Location.X,
                Y = entity.Location.Y,
                Z = entity.Location.Z,
                Health = (ushort)entity.Health
            });
        }

        public void SendParticleSpawn(ParticleType particleType, Vector3 location, float[] data, PlayerConnection connection)
        {
            connection.SendPacket(new ParticleSpawnPacket()
            {
                ParticleType = (ushort)particleType,
                X = location.X,
                Y = location.Y,
                Z = location.Z,
                Data = data
            });
        }

        void SendEntityMovement(Entity entity, PlayerConnection connection)
        {
            connection.SendPacket(new EntityMovementPacket()
            {
                Id = entity.Id,
                X = entity.Location.X,
                Y = entity.Location.Y,
                Z = entity.Location.Z
            }, DeliveryMethod.ReliableSequenced);
        }

        void SendPlayerMovement(Player[] players, PlayerConnection connection)
        {
            var playerMovements = players.Select(x => (PlayerMovement)x.Movement);
            connection.SendPacket(new ServerPlayerMovementPacket()
            {
                Tick = connection.CurrentGameRoom.Tick,
                Id = players.Select(x => x.Id).ToArray(),
                X = players.Select(x => x.Location.X).ToArray(),
                Y = players.Select(x => x.Location.Y).ToArray(),
                Z = players.Select(x => x.Location.Z).ToArray(),
                Horizontal = playerMovements.Select(x => x.Horizontal).ToArray()
            }, DeliveryMethod.Unreliable);
        }

        void SendEntityDeath(Entity entity, PlayerConnection connection)
        {
            connection.SendPacket(new EntityDeathPacket()
            {
                Id = entity.Id
            });
        }

        void SendEntityHealthChange(Entity entity, PlayerConnection connection)
        {
            connection.SendPacket(new EntityHealthUpdatePacket()
            {
                Id = entity.Id,
                Health = (ushort)entity.Health
            }, DeliveryMethod.ReliableSequenced);
        }
        
        void SendPlayerSpellUse(Player player, SpellInfo spellInfo, PlayerConnection connection)
        {
            connection.SendPacket(new PlayerSpellUsePacket()
            {
                Id = player.Id,
                SpellType = (ushort)spellInfo.Type,
                Cooldown = spellInfo.Cooldown
            });
        }

        public void SendCountdownPacket()
        {
            foreach (var item in gameRoom.Players)
            {
                var connection = item.Value;
                connection.SendPacket(new CountdownPacket()
                {
                    StartCountdown = true
                });
            }
        }

        public void SendServerTickPacket()
        {
            foreach (var item in gameRoom.Players)
            {
                var connection = item.Value;
                connection.SendPacket(new ServerTickPacket()
                {
                    CurrentTick = gameRoom.Tick
                });
            }
        }

        public void SendEntityInfoUpdate(Entity entity, string newInfo)
        {
            foreach (var item in gameRoom.Players)
            {
                var connection = item.Value;
                connection.SendPacket(new EntityInfoUpdatePacket()
                {
                    Id = entity.Id,
                    NewInfo = newInfo,
                    X = entity.Location.X,
                    Y = entity.Location.Y,
                    Z = entity.Location.Z
                });
            }
        }
    }
}
