using Ara3D;
using SideWars.Shared.Game;
using SideWars.Shared.Packets;
using SideWarsServer.Game.Logic.Spells;
using SideWarsServer.Networking;

namespace SideWarsServer.Game.Logic.Champions
{
    public class Desgama : Player
    {
        public Desgama(Vector3 location, PlayerConnection playerConnection, EntityTeam team) : base(location, PlayerInfo.Desgama, playerConnection, team)
        {
            PlayerSpells = new DesgamaSpells();
        }
    }
}
