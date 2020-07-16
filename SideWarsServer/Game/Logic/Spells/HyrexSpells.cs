using SideWars.Shared.Game;
using SideWarsServer.Game.Room;
using SideWarsServer.Utils;
using System;

namespace SideWarsServer.Game.Logic.Spells
{
    public class HyrexSpells : PlayerSpells
    {
        public HyrexSpells() : base()
        {
            SpellInfo = new MarkSpellInfo();
        }

        public override bool Cast(IGameRoom gameRoom, Player player, SpellInfo spell)
        {
            var canCast = base.Cast(gameRoom, player, spell);

            if (canCast)
            {
                var baseGameRoom = (BaseGameRoom)gameRoom;

                if (spell.Type == SpellType.HyrexSlide)
                {
                
                }
            }

            return canCast;
        }
    }
}
