using SideWars.Shared.Game;
using SideWarsServer.Game.Logic.StatusEffects;
using SideWarsServer.Game.Room;
using SideWarsServer.Utils;
using System;
using System.Linq;

namespace SideWarsServer.Game.Logic.Spells
{
    public class PlayerSpells : IPlayerSpells
    {
        public SpellTimer SpellTimer { get; set; }
        public PlayerSpellInfo SpellInfo { get; set; }
        private bool isPaused;

        public PlayerSpells()
        {
            isPaused = false;
            SpellTimer = new SpellTimer();
        }

        public virtual bool Cast(IGameRoom gameRoom, Player player, SpellInfo spell)
        {
            if (!SpellInfo.Spells.Contains(spell))
                throw new Exception("Character doesn't have the spell " + spell.Type);

            if (isPaused)
                return false;

            if (!SpellTimer.CanCast(spell))
                return false;

            // Do not cast if player is muted
            if (player.StatusEffects.OfType<MutedStatusEffect>().Any())
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
