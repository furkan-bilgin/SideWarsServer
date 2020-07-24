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
        public PlayerConnection PlayerConnection { get; set; }

        public Player(Vector3 location, PlayerInfo playerInfo, PlayerConnection playerConnection, EntityTeam team) : base(playerInfo, team)
        {
            PlayerInfo = playerInfo;
            EntityInfo = PlayerInfo;

            PlayerConnection = playerConnection;
            Location = location;
            
            Collider = new SquareCollider(location, PlayerInfo.HitBoxMin, PlayerInfo.HitBoxMax);
            Movement = new PlayerMovement(Team, Collider, PlayerInfo.Speed);
            PlayerCombat = new PlayerCombat(PlayerInfo);

            UpdateEntityInfo(playerInfo);
        }

        public override void UpdateEntityInfo(EntityInfo entityInfo)
        {
            base.UpdateEntityInfo(entityInfo);
            
            var playerInfo = (PlayerInfo)entityInfo;

            Movement.Speed = playerInfo.Speed;
            PlayerCombat.ReInitialize(playerInfo);
        }
    }
}
