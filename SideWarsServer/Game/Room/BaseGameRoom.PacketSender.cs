using LiteNetLib;
using SideWars.Shared.Packets;
using SideWars.Shared.Physics;
using SideWarsServer.Game.Logic;
using SideWarsServer.Utils;
using System;
using System.Collections.Generic;

namespace SideWarsServer.Game.Room
{
    public partial class BaseGameRoom
    {
        void SendEntitySpawn(Entity entity, NetPeer peer)
        {
            var data = new List<ushort>();

            if (entity is Player)
            {
                var player = (Player)entity;
                data.Add((ushort)player.PlayerInfo.PlayerType);
                
                if (player.PlayerConnection.NetPeer.Id == peer.Id) // If peer has the same id as the entity, that means he can control it
                {
                    data.Add((ushort)EntityData.Controllable);
                }
            }
            else if (entity is Projectile)
            {
                var projectile = (Projectile)entity;
                data.Add((ushort)projectile.ProjectileInfo.Type);
            }

            Server.Instance.NetworkController.SendPacket(peer, new EntitySpawnPacket()
            {
                Id = entity.Id,
                EntityType = (byte)entity.Type,
                Data = data.ToArray(),
                Team = (byte)entity.Team,
                X = entity.Location.X,
                Y = entity.Location.Y,
                Z = entity.Location.Z,
                Health = (ushort)entity.Health
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
    }
}
