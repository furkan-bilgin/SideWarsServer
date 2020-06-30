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

        public PlayerCombat(PlayerInfo playerInfo)
        {
            attackTimer = new Timer(playerInfo.AttackSpeed);
            this.playerInfo = playerInfo;
        }

        public virtual bool Shoot()
        {
            return attackTimer.CanTick();
        }
    }
}
