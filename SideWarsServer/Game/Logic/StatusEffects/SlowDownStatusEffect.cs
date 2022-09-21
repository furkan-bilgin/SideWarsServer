using SideWars.Shared.Game;
using SideWarsServer.Utils;

namespace SideWarsServer.Game.Logic.StatusEffects
{
    public class SlowdownStatusEffect : IStatusEffect
    {
        public int ExpirityPeriod { get; set; }
        public int SpawnTick { get; private set; }
        private int slowdownPercentage;

        public SlowdownStatusEffect(int expirityPeriod, int spawnTick, int slowdownPercentage)
        {
            ExpirityPeriod = expirityPeriod;
            SpawnTick = spawnTick;
            this.slowdownPercentage = slowdownPercentage;
        }

        public EntityInfo ApplyEffect(EntityInfo entityInfo)
        {
            var playerInfo = (PlayerInfo)entityInfo;
            playerInfo.Speed = playerInfo.Speed.DownByPercentage(slowdownPercentage); 

            return playerInfo;
        }
    }
}
