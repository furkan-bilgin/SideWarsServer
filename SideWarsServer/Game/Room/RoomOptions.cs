using Ara3D;
using SideWars.Shared.Packets;
using System.IO;

namespace SideWarsServer.Game.Room
{
    public class RoomOptions
    {
        public static RoomOptions Default = new RoomOptions()
        {
            MaxPlayers = 1,
            MaxScore = 2,
            RoundTime = 30,
            BlueSpawnPoint = new Vector3(0, 1, 0),
            RedSpawnPoint = new Vector3(0, 1, 14)
        };
        
        public int MaxPlayers { get; set; }
        public int MaxScore { get; set; }
        public int RoundTime { get; set; }
        public Vector3 RedSpawnPoint { get; set; }
        public Vector3 BlueSpawnPoint { get; set; }

        public Vector3 GetSpawnPoint(EntityTeam team)
        {
            return team == EntityTeam.Blue ? BlueSpawnPoint : RedSpawnPoint;
        }
    }
}
