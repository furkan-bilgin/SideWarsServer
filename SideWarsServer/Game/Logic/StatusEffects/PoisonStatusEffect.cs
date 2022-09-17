using SideWars.Shared.Game;
using SideWars.Shared.Utils;
using SideWarsServer.Utils;

namespace SideWarsServer.Game.Logic.StatusEffects
{
    public class PoisonStatusEffect : IStatusEffect
    {
        public int ExpirityPeriod { get; set; }
        public int SpawnTick { get; private set; }

        public int DamagePerUnitTime;
        private Timer timer;

        public PoisonStatusEffect(int expirityPeriod, int spawnTick, int damagePerUnitTime, float timeInSeconds)
        {
            ExpirityPeriod = expirityPeriod;
            SpawnTick = spawnTick;
            DamagePerUnitTime = damagePerUnitTime;

            timer = new Timer((int)(timeInSeconds * 1000f));
        }

        public bool CanPoison()
        {
            return timer.CanTick();
        }

        public EntityInfo ApplyEffect(EntityInfo entityInfo)
        {
            return entityInfo;
        }
    }
}
