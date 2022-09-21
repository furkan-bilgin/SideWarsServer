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
            var playerInfo = (PlayerInfo)entityInfo;
            playerInfo.Speed = playerInfo.Speed.DownByPercentage(100);
            playerInfo.AttackSpeed = playerInfo.AttackSpeed.DownByPercentage(100);

            return playerInfo;
        }
    }
}
