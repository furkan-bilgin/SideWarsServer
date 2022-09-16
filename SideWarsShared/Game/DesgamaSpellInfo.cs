using System;
using System.Collections.Generic;
using System.Text;

namespace SideWars.Shared.Game
{
    public class DesgamaSpellInfo : PlayerSpellInfo
    {
        public DesgamaSpellInfo()
        {
            Spells = new List<SpellInfo>()
            {
                new SpellInfo(10, SpellType.DesgamaMissile),
                new SpellInfo(15, SpellType.DesgamaShield)
            };
        }
    }
}
