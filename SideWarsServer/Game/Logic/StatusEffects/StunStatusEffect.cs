using SideWars.Shared.Game;
using SideWars.Shared.Utils;
using SideWarsServer.Utils;

namespace SideWarsServer.Game.Logic.StatusEffects
{
    public class StunStatusEffect : IStatusEffect
    {
        public int ExpirityPeriod { get; set; }
        public int SpawnTick { get; private set; }

        public StunStatusEffect(int expirityPeriod, int spawnTick)
        {
            ExpirityPeriod = expirityPeriod;
            SpawnTick = spawnTick;
        }

        public EntityInfo ApplyEffect(EntityInfo entityInfo)
        {
            return entityInfo;
        }
    }
}
