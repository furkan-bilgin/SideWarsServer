using Ara3D;
using SideWarsServer.Game.Logic;
using SideWarsServer.Game.Room.Listener;
using SideWarsServer.Networking;
using SideWars.Shared.Game;
using System.Collections.Generic;
using SideWarsServer.Game.Logic.Scheduler;
using SideWarsServer.Game.Logic.GameLoop;
using System;

namespace SideWarsServer.Game.Room
{
    public interface IGameRoom : IDisposable
    {
        RoomScheduler RoomScheduler { get; set; }
        ProjectileSpawner ProjectileSpawner { get; set; }
        RoomOptions RoomOptions { get; }
        GameRoomState RoomState { get; set; }
        IGameRoomListener Listener { get; set; }
        Dictionary<int, Entity> Entities { get; set; }
        Dictionary<int, PlayerConnection> Players { get; set; }
        BaseGameRoomPacketSender PacketSender { get; set; }
        int Tick { get; }
        int CurrentRound { get; }

        void AddPlayer(PlayerConnection playerConnection);
        void RemovePlayer(PlayerConnection playerConnection);
        Entity SpawnEntity(Entity entity);
        void SpawnParticle(ParticleType particleType, Vector3 location, float[] data = null);
        T GetGameLoop<T>() where T : IGameLoop;
        List<Entity> GetEntities();
    }
}
