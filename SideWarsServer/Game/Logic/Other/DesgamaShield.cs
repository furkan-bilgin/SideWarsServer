using Ara3D;
using SideWars.Shared.Game;
using SideWars.Shared.Packets;
using SideWarsServer.Game.Logic.StatusEffects;

namespace SideWarsServer.Game.Logic.Other
{
    public class DesgamaShield : Entity
    {
        public static EntityInfo DesgamaShieldInfo => new EntityInfo()
        {
            BaseHealth = 90,
            HitBoxMin = -new Vector3(1, 1, 0.375f),
            HitBoxMax = new Vector3(1, 1, 0.375f)
        };

        public DesgamaShield(Player player) : base(DesgamaShieldInfo, player.Team)
        {
            Type = EntityType.DesgamaShield;
            var invertZ = player.Team == EntityTeam.Red;

            Location = player.Location + new Vector3(0, 0.2f, invertZ ? -2 : 2);

            // Apply a poison status effect forever
            StatusEffects.Add(new PoisonStatusEffect(999999, 0, 15, 1f));
            UpdateEntityInfo(EntityInfo);
        }
    }
}
