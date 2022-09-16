#pragma warning disable CS1998
using SideWars.Shared.Game;
using SideWarsServer.Database.Models;
using System.Threading.Tasks;

namespace SideWarsServer.Database
{
    public class DebugTokenController : ITokenController
    {
        ChampionType a = ChampionType.Desgama;
        public async Task<Token> GetTokenAsync(string token)
        {
            //a = a == ChampionType.Mark ? ChampionType.Hyrex : ChampionType.Mark;
            return new Token(token.GetHashCode(), "Player", "default_room" + token.GetHashCode(), a);
        }
    }
}
