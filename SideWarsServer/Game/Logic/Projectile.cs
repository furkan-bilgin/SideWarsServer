using Ara3D;
using SideWars.Shared.Game;
using SideWars.Shared.Physics;
using SideWarsServer.Game.Logic.Combat;
using SideWarsServer.Networking;

namespace SideWarsServer.Game.Logic
{
    public class Projectile : Entity
    {
        public PlayerInfo ShooterInfo { get; set; }
        public ProjectileInfo ProjectileInfo { get; set; }
        
        public Projectile(Vector3 location, PlayerInfo shooterInfo)
        {
            ShooterInfo = PlayerInfo.Default;

            Location = location;
            //Collider = new SquareCollider(location, PlayerInfo.HitBoxMin, PlayerInfo.HitBoxMax); 
        }
    }
}
