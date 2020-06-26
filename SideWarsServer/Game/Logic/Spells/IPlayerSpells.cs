using SideWars.Shared.Game;
using SideWarsServer.Game.Room;

namespace SideWarsServer.Game.Logic.Spells
{
    public interface IPlayerSpells
    {
        PlayerSpellInfo SpellInfo { get; set; }
        SpellTimer SpellTimer { get; set; }

        bool Cast(IGameRoom gameRoom, Player player, SpellInfo spell);
    }
}
