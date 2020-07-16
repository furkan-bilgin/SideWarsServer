using Ara3D;
using SideWars.Shared.Game;
using SideWars.Shared.Packets;
using SideWarsServer.Game.Logic.Spells;
using SideWarsServer.Networking;

namespace SideWarsServer.Game.Logic.Champions
{
    public class Hyrex : Player
    {
        public Hyrex(Vector3 location, PlayerConnection playerConnection, EntityTeam team) : base(location, PlayerInfo.Mark, playerConnection, team)
        {
            PlayerSpells = new HyrexSpells();
        }
    }
}
