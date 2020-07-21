using SideWars.Shared.Game;
using SideWarsServer.Utils;

namespace SideWarsServer.Game.Logic.StatusEffects
{
    public class HyrexFastFireStatusEffect : IStatusEffect
    {
        public int ExpirityPeriod { get; set; }
        public int SpawnTick { get; private set; }

        public HyrexFastFireStatusEffect(int expirityPeriod, int spawnTick)
        {
            ExpirityPeriod = expirityPeriod;
            SpawnTick = spawnTick;
        }

        public EntityInfo ApplyEffect(EntityInfo entityInfo)
        {
            var playerInfo = (PlayerInfo)entityInfo;
            playerInfo.AttackSpeed = playerInfo.AttackSpeed.DownByPercentage(50); // Fire 50% faster

            return playerInfo;
        }
    }
}
