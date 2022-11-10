using SideWars.Shared.Game;
using SideWarsServer.Utils;

namespace SideWarsServer.Networking
{
    public class MockPlayerConnection : PlayerConnection
    {
        public MockPlayerConnection(string roomId, ChampionType championType) : base(new Database.Models.Token(RandomTool.Current.Int(0, 999999), "MockPlayer", roomId, championType, SideWars.Shared.Packets.EntityTeam.Blue), null)
        {
            IsReady = true;
        }

        public MockPlayerConnection(ChampionType championType) : this(RandomTool.Current.String(25), championType) { }
    }
}
