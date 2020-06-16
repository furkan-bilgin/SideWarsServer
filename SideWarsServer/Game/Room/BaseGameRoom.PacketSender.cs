using LiteNetLib;
using SideWars.Shared.Packets;
using SideWarsServer.Game.Logic;
using System;
using System.Collections.Generic;

namespace SideWarsServer.Game.Room
{
    public partial class BaseGameRoom
    {
        void SendEntitySpawn(Entity entity, NetPeer peer)
        {
            var data = new List<byte>();

            if (entity is Player)
            {
                var player = (Player)entity;
                if (player.PlayerConnection.NetPeer.Id == peer.Id) // If peer has the same id as the entity, that means he can control it
                {
                    data.Add((byte)EntityData.Controllable);
                }
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
    }
}
