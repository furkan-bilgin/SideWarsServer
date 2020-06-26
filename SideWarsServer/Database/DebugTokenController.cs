#pragma warning disable CS1998
using SideWarsServer.Database.Models;
using System.Threading.Tasks;

namespace SideWarsServer.Database
{
    public class DebugTokenController : ITokenController
    {
        public async Task<Token> GetTokenAsync(string token)
        {
            return new Token(true, "Player", token, SideWars.Shared.Game.ChampionType.Mark);
        }
    }
}
