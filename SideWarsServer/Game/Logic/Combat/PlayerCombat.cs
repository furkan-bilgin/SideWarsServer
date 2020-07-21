using SideWars.Shared.Game;
using SideWars.Shared.Utils;
using SideWarsServer.Utils;
using System.Diagnostics;

namespace SideWarsServer.Game.Logic.Combat
{
    public class PlayerCombat
    {
        private PlayerInfo playerInfo;
        private Timer attackTimer;
        private bool isPaused;

        public PlayerCombat(PlayerInfo playerInfo)
        {
            ReInitialize(playerInfo);
            this.playerInfo = playerInfo;
        }

        public void ReInitialize(PlayerInfo playerInfo)
        {
            if (attackTimer == null)
                attackTimer = new Timer(playerInfo.AttackSpeed);
            else
                attackTimer.PeriodMilliseconds = playerInfo.AttackSpeed;

            this.playerInfo = playerInfo;
        }

        public virtual bool Shoot()
        {
            if (isPaused)
                return false;

            if (playerInfo.AttackSpeed <= 0)
                return false;

            return attackTimer.CanTick();
        }

        public void Pause()
        {
            isPaused = true;
        }

        public void Resume()
        {
            isPaused = false;
        }
    }
}
