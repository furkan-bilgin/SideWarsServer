using Ara3D;
using SideWars.Shared.Game;
using SideWars.Shared.Packets;
using SideWars.Shared.Physics;
using SideWarsServer.Game.Logic.Combat;
using SideWarsServer.Game.Logic.Spells;
using SideWarsServer.Networking;

namespace SideWarsServer.Game.Logic
{
    public class Player : Entity
    {
        public IPlayerSpells PlayerSpells { get; set; }
        public PlayerInfo PlayerInfo { get; set; }
        public PlayerCombat PlayerCombat { get; set; }
        public PlayerConnection PlayerConnection { get; private set; }

        public Player(Vector3 location, PlayerInfo playerInfo, PlayerConnection playerConnection, EntityTeam team) : base(playerInfo)
        {
            PlayerInfo = playerInfo;
            Team = team;

            Health = PlayerInfo.BaseHealth;

            PlayerConnection = playerConnection;
            Location = location;
            
            Collider = new SquareCollider(location, PlayerInfo.HitBoxMin, PlayerInfo.HitBoxMax);
            Movement = new PlayerMovement(Team, Collider, PlayerInfo.Speed, PlayerInfo.JumpSpeed);
            PlayerCombat = new PlayerCombat(PlayerInfo);
        }
    }
}
