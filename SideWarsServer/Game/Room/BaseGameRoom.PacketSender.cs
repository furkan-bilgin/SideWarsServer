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

namespace SideWarsServer.Game.Room
{
    public partial class BaseGameRoom
    {
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
                        if (Tick % LogicTimer.FramesPerSecond == 0) // Send Player positions every second in case of sync issues. 
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

        void SendEntityHealthChangePackets(Entity entity)
        {
            foreach (var player in Players)
            {
                SendEntityHealthChange(entity, player.Value.NetPeer);
            }
        }

        void SendEntityDeathPackets()
        {
            foreach (var entity in deadEntities)
            {
                foreach (var player in Players)
                {
                    SendEntityDeath(entity, player.Value.NetPeer);
                }
            }
        }

        ///////// PACKET SENDER FUNCTIONS /////////

        void SendEntitySpawn(Entity entity, NetPeer peer)
        {
            var data = new List<ushort>();
            var bigData = new List<float>();

            if (entity is Player)
            {
                var player = (Player)entity;
                data.Add((ushort)player.PlayerInfo.PlayerType);
                
                if (player.PlayerConnection.NetPeer.Id == peer.Id) // If peer has the same id as the entity, that means he can control it
                {
                    data.Add((ushort)EntityData.Controllable);
                }
            }
            else if (entity is Grenade)
            {
                var grenade = (Grenade)entity;
                bigData.Add(grenade.Target.X);
                bigData.Add(grenade.Target.Y);
                bigData.Add(grenade.Target.Z);
            }

            Server.Instance.NetworkController.SendPacket(peer, new EntitySpawnPacket()
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

        void SendParticleSpawn(ParticleType particleType, Vector3 location, float[] data, NetPeer peer)
        {
            Server.Instance.NetworkController.SendPacket(peer, new ParticleSpawnPacket()
            {
                ParticleType = (ushort)particleType,
                X = location.X,
                Y = location.Y,
                Z = location.Z,
                Data = data
            });
        }

        void SendEntityMovement(Entity entity, NetPeer peer)
        {
            Server.Instance.NetworkController.SendPacket(peer, new EntityMovementPacket()
            {
                Id = entity.Id,
                X = entity.Location.X,
                Y = entity.Location.Y,
                Z = entity.Location.Z
            }, DeliveryMethod.Unreliable);
        }

        void SendPlayerMovement(Player player, NetPeer peer)
        {
            var playerMovement = (PlayerMovement)player.Movement;
            Server.Instance.NetworkController.SendPacket(peer, new ServerPlayerMovementPacket()
            {
                Id = player.Id,
                Horizontal = Functions.AsSByte(playerMovement.Horizontal),
                Jump = Convert.ToBoolean(playerMovement.Jump)
            }, DeliveryMethod.Unreliable);
        }

        void SendEntityDeath(Entity entity, NetPeer peer)
        {
            Server.Instance.NetworkController.SendPacket(peer, new EntityDeathPacket()
            {
                Id = entity.Id
            }, DeliveryMethod.Unreliable);
        }

        void SendEntityHealthChange(Entity entity, NetPeer peer)
        {
            Server.Instance.NetworkController.SendPacket(peer, new EntityHealthUpdatePacket()
            {
                Id = entity.Id,
                Health = (ushort)entity.Health
            }, DeliveryMethod.ReliableSequenced);
        }
    }
}
