using System.IO;

namespace SideWarsServer.Game.Room
{
    public class RoomOptions
    {
        public static RoomOptions Default = new RoomOptions()
        {
            MaxPlayers = 2
        };
        
        public int MaxPlayers { get; set; }
    }
}
