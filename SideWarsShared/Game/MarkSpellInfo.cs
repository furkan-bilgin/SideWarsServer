using System;
using System.Collections.Generic;
using System.Text;

namespace SideWars.Shared.Game
{
    public class MarkSpellInfo : PlayerSpellInfo
    {
        public MarkSpellInfo()
        {
            Spells = new List<SpellInfo>()
            {
                new SpellInfo(10, SpellType.MarkGrenade), 
                new SpellInfo(15, SpellType.MarkHeal)  
            };
        }
    }
}
