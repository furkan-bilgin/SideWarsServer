using Ara3D;
using SideWars.Shared.Game;
using SideWars.Shared.Physics;
using SideWarsServer.Game.Logic.Combat;
using SideWarsServer.Networking;

namespace SideWarsServer.Game.Logic
{
    public class Player : Entity
    {
        public PlayerInfo PlayerInfo { get; set; }
        public PlayerCombat PlayerCombat { get; set; }
        public PlayerConnection PlayerConnection { get; private set; }

        public Player(Vector3 location, PlayerConnection playerConnection)
        {
            PlayerInfo = PlayerInfo.Default;

            Location = location;
            Collider = new SquareCollider(location, PlayerInfo.HitBoxMin, PlayerInfo.HitBoxMax); // TODO
            PlayerConnection = playerConnection;
            
            Movement = new PlayerMovement(Team, Collider, 5); // TODO
            
            PlayerCombat = new PlayerCombat(PlayerInfo);
        }
    }
}
