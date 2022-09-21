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

        public void SendMovementPackets()
        {
            foreach (var playerItem in gameRoom.Players)
            {
                foreach (var entityItem in gameRoom.Entities)
                {
                    var entity = entityItem.Value;
                    void sendEntityMovement()
                    {
                        SendEntityMovement(entity, playerItem.Value);
                    }

                    if (entity is Player)
                    {
                        if (gameRoom.Tick % LogicTimer.FramesPerSecond / 3 == 0) // Send Player positions thrice a second for sync issues. 
                            sendEntityMovement();
                    }
                }

            }
        }

        public void SendPlayerMovementPackets()
        {
            foreach (var playerItem in gameRoom.Players)
            {
                foreach (var item in gameRoom.GetEntities().Where(x => x is Player).Select(x => (Player)x))
                {
                    if (item.PlayerConnection.Token.ID != playerItem.Value.Token.ID)
                    {
                        SendPlayerMovement(item, item.PlayerConnection);
                    }
                }
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

        void SendPlayerMovement(Player player, PlayerConnection connection)
        {
            var playerMovement = (PlayerMovement)player.Movement;
            connection.SendPacket(new ServerPlayerMovementPacket()
            {
                Id = player.Id,
                Horizontal = Functions.AsSByte(playerMovement.Horizontal)
            });
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
            }, DeliveryMethod.ReliableOrdered);
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
