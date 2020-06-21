using SideWars.Shared.Game;
using System.Diagnostics;

namespace SideWarsServer.Game.Logic.Combat
{
    public class PlayerCombat
    {
        private PlayerInfo playerInfo;
        private Stopwatch attackTimer;

        public PlayerCombat(PlayerInfo playerInfo)
        {
            attackTimer = new Stopwatch();
            
            this.playerInfo = playerInfo;
        }

        public virtual bool Shoot()
        {
            if (!attackTimer.IsRunning)
            {
                attackTimer.Start();
                return true;
            }

            if (attackTimer.ElapsedMilliseconds >= playerInfo.AttackSpeed)
            {
                attackTimer.Reset();
                attackTimer.Start();

                return true;
            }

            return false;
        }

        public virtual Entity CreateProjectile()
        {
            switch (playerInfo.ProjectileType)
            {
                //case ProjectileType.Default:
                //    return new Bullet();

            }

            return null;
        }
    }
}
