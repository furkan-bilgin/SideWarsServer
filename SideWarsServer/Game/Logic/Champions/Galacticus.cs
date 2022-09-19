using Ara3D;
using SideWars.Shared.Game;
using SideWars.Shared.Packets;
using SideWarsServer.Game.Logic.Spells;
using SideWarsServer.Networking;

namespace SideWarsServer.Game.Logic.Champions
{
    public class Galacticus : Player
    {
        public Galacticus(Vector3 location, PlayerConnection playerConnection, EntityTeam team) : base(location, PlayerInfo.Galacticus, playerConnection, team)
        {
            PlayerSpells = new GalacticusSpells();
        }
    }
}
