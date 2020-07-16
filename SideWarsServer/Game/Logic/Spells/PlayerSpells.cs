using SideWars.Shared.Game;
using SideWarsServer.Game.Room;
using SideWarsServer.Utils;
using System;

namespace SideWarsServer.Game.Logic.Spells
{
    public class PlayerSpells : IPlayerSpells
    {
        public SpellTimer SpellTimer { get; set; }
        public PlayerSpellInfo SpellInfo { get; set; }
        private bool isPaused;

        public PlayerSpells()
        {
            SpellTimer = new SpellTimer();
        }

        public virtual bool Cast(IGameRoom gameRoom, Player player, SpellInfo spell)
        {
            if (!SpellInfo.Spells.Contains(spell))
                throw new Exception("Mark doesn't have the spell " + spell.Type);

            if (isPaused)
                return false;

            if (!SpellTimer.CanCast(spell))
                return false;

            return true;
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
