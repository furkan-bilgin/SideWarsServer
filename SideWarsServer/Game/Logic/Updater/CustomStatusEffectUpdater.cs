using SideWarsServer.Game.Logic.StatusEffects;
using SideWarsServer.Game.Room;
namespace SideWarsServer.Game.Logic.Updater
{
    public class CustomStatusEffectUpdater : IEntityUpdater
    {
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
        }
    }
}
