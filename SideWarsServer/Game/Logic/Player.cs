using Ara3D;
using SideWars.Shared.Physics;
using SideWarsServer.Networking;

namespace SideWarsServer.Game.Logic
{
    public class Player : Entity
    {
        public PlayerConnection PlayerConnection { get; private set; }
        public IPlayerMovement PlayerMovement { get; private set; }

        public Player(Vector3 location, PlayerConnection playerConnection)
        {
            Location = location;
            Collider = new SquareCollider(location, -Vector3.One, Vector3.One); // TODO
            PlayerConnection = playerConnection;
            PlayerMovement = new PlayerMovement(Team, 5); // TODO
        }
    }
}
