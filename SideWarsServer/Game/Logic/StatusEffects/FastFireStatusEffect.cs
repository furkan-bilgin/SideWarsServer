using SideWars.Shared.Game;
using SideWarsServer.Utils;

namespace SideWarsServer.Game.Logic.StatusEffects
{
    public class FastFireStatusEffect : IStatusEffect
    {
        public int ExpirityPeriod { get; set; }
        public int SpawnTick { get; private set; }
        private int fastFirePercentage;

        public FastFireStatusEffect(int expirityPeriod, int spawnTick, int fastFirePercentage)
        {
            ExpirityPeriod = expirityPeriod;
            SpawnTick = spawnTick;
            this.fastFirePercentage = fastFirePercentage;
        }

        public EntityInfo ApplyEffect(EntityInfo entityInfo)
        {
            var playerInfo = (PlayerInfo)entityInfo;
            playerInfo.AttackTime = playerInfo.AttackTime.DownByPercentage(fastFirePercentage); // Fire 50% faster

            return playerInfo;
        }
    }
}
