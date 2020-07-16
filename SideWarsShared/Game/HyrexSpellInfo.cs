using System;
using System.Collections.Generic;
using System.Text;

namespace SideWars.Shared.Game
{
    public class HyrexSpellInfo : PlayerSpellInfo
    {
        public HyrexSpellInfo()
        {
            Spells = new List<SpellInfo>()
            {
                new SpellInfo(8, SpellType.HyrexSlide),
                new SpellInfo(10, SpellType.HyrexFastFire)    
            };
        }
    }
}
