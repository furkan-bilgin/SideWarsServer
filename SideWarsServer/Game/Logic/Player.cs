using Ara3D;
using SideWarsServer.Game.Logic.Physics;
using SideWarsServer.Networking;

namespace SideWarsServer.Game.Logic
{
    public class Player
    {
        public Vector3 Location { get; set; }
        public SquareCollider Collider { get; set; }
        public PlayerConnection PlayerConnection { get; private set; }

        public Player(Vector3 location, PlayerConnection playerConnection)
        {
            Location = location;
            Collider = new SquareCollider(location, -Vector3.One, Vector3.One); // TODO
            PlayerConnection = playerConnection;
        }
    }
}
