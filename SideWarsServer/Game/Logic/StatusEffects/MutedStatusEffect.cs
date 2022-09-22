using SideWars.Shared.Game;
using SideWarsServer.Utils;

namespace SideWarsServer.Game.Logic.StatusEffects
{
    public class MutedStatusEffect : IStatusEffect
    {
        public int ExpirityPeriod { get; set; }
        public int SpawnTick { get; private set; }

        public MutedStatusEffect(int expirityPeriod, int spawnTick)
        {
            ExpirityPeriod = expirityPeriod;
            SpawnTick = spawnTick;
        }

        public EntityInfo ApplyEffect(EntityInfo entityInfo)
        {
            var playerInfo = (PlayerInfo)entityInfo;
            playerInfo.AttackTime = playerInfo.AttackTime.DownByPercentage(100);

            return playerInfo;
        }
    }
}
