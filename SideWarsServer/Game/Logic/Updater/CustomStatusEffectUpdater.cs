using SideWarsServer.Game.Logic.GameLoop;
using SideWarsServer.Game.Logic.StatusEffects;
using SideWarsServer.Game.Room;
using System.Collections.Generic;
using System.Linq;

namespace SideWarsServer.Game.Logic.Updater
{
    public class CustomStatusEffectUpdater : IEntityUpdater
    {
        private HashSet<Entity> stunnedEntities;

        public CustomStatusEffectUpdater()
        {
            stunnedEntities = new HashSet<Entity>();
        }

        public void Update(Entity entity, IGameRoom gameRoom)
        {
            foreach (var statusEffect in entity.StatusEffects)
            {
                if (statusEffect is PoisonStatusEffect)
                {
                    var poisonStatusEffect = (PoisonStatusEffect)statusEffect;

                    // Damage entity if we can poison
                    if (poisonStatusEffect.CanPoison())
                    {
                        entity.Hurt(poisonStatusEffect.DamagePerUnitTime);
                    }
                }
            }

            UpdateStun(entity, gameRoom);
        }

        private void UpdateStun(Entity entity, IGameRoom gameRoom)
        {
            if (entity.Movement == null)
                return;

            var baseGameRoom = (BaseGameRoom)gameRoom;
            var hasStunStatusEffect = entity.StatusEffects.OfType<StunStatusEffect>().Any();
            entity.Movement.IsHalted = hasStunStatusEffect;

            // Send stun on change
            if (hasStunStatusEffect && !stunnedEntities.Contains(entity))
            {
                baseGameRoom.GetGameLoop<PacketSenderGameLoop>().OnEntityHaltChange(entity);
                stunnedEntities.Add(entity);
            } 
            else if (!hasStunStatusEffect && stunnedEntities.Contains(entity)) 
            {
                baseGameRoom.GetGameLoop<PacketSenderGameLoop>().OnEntityHaltChange(entity);
                stunnedEntities.Remove(entity);
            }
        }
    }
}
