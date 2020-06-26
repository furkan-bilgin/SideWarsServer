using SideWars.Shared.Packets;
using System.Collections.Generic;
using System.Linq;

namespace SideWars.Shared.Game
{
    public class PlayerSpellInfo
    {
        public List<SpellInfo> Spells { get; set; }

        public SpellInfo GetSpellInfo(SpellType type)
        {
            return Spells.Where(x => x.Type == type).First();
        }
        
        public SpellInfo GetSpellInfo(PlayerButton button)
        {
            return button == PlayerButton.Special1 ? Spells[0] : Spells[1];
        }
    }
}
