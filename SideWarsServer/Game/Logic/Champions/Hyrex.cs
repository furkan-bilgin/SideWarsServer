using Ara3D;
using SideWars.Shared.Game;
using SideWars.Shared.Packets;
using SideWars.Shared.Physics;
using SideWarsServer.Game.Logic.Spells;
using SideWarsServer.Networking;

namespace SideWarsServer.Game.Logic.Champions
{
    public class Hyrex : Player
    {
        public Hyrex(Vector3 location, PlayerConnection playerConnection, EntityTeam team) : base(location, PlayerInfo.Hyrex, playerConnection, team)
        {
            Movement = new HyrexMovement(Team, Collider, PlayerInfo.Speed);
            PlayerSpells = new HyrexSpells();
        }
    }
}
