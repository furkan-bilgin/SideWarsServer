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
            attackTimer = new Timer(playerInfo.AttackSpeed);
            this.playerInfo = playerInfo;
        }

        public virtual bool Shoot()
        {
            if (isPaused)
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
