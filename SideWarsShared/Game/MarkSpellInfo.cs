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
                new SpellInfo(10, SpellType.MarkGrenade), // Grenade skill, 10 sec cooldown
                new SpellInfo(15, SpellType.MarkHeal)     // Heal skill, 10 sec cooldown
            };
        }
    }
}
