using System;
using System.Collections.Generic;
using System.Text;

namespace SideWars.Shared.Game
{
    public class GalacticusSpellInfo : PlayerSpellInfo
    {
        public GalacticusSpellInfo()
        {
            Spells = new List<SpellInfo>()
            {
                new SpellInfo(8, SpellType.GalacticusSlow),
                new SpellInfo(15, SpellType.GalacticusStun)
            };
        }
    }
}
