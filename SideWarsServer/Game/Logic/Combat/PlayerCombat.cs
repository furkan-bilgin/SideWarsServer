using SideWars.Shared.Game;
using SideWars.Shared.Utils;
using SideWarsServer.Utils;
using System.Diagnostics;

namespace SideWarsServer.Game.Logic.Combat
{
    public class PlayerCombat
    {
        public int CurrentAmmo { get; private set; }

        private PlayerInfo playerInfo;
        private FloatTimer attackTimer;
        private FloatTimer reloadTimer;
        private bool isPaused;

        public PlayerCombat(PlayerInfo playerInfo)
        {
            CurrentAmmo = playerInfo.AmmoCount;

            Initialize(playerInfo);
            this.playerInfo = playerInfo;
        }

        public void Initialize(PlayerInfo playerInfo)
        {
            if (attackTimer == null)
                attackTimer = new FloatTimer(playerInfo.AttackTime);
            else
                attackTimer.Period = playerInfo.AttackTime;

            reloadTimer = new FloatTimer(playerInfo.ReloadTime);
            this.playerInfo = playerInfo;
        }

        public virtual bool Shoot()
        {
            if (isPaused)
                return false;

            if (playerInfo.AttackTime == 0)
                return false;

            if (CurrentAmmo == 0)
            {
                if (reloadTimer.CanTick())
                {
                    CurrentAmmo = playerInfo.AmmoCount;
                    return Shoot();
                }
                else
                {
                    return false;
                }
            }

            var canAttack = attackTimer.CanTick();
            if (canAttack)
            {
                CurrentAmmo--;
                // Exhaust timer to restart it
                if (CurrentAmmo == 0)
                    reloadTimer.CanTick();
            }

            return canAttack;
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
