#pragma warning disable CS1998
using SideWars.Shared.Game;
using SideWarsServer.Database.Models;
using System.Threading.Tasks;

namespace SideWarsServer.Database
{
    public class DebugTokenController : ITokenController
    {
        ChampionType a = ChampionType.Mark;
        public async Task<Token> GetTokenAsync(string token)
        {
            //a = a == ChampionType.Desgama ? ChampionType.Mark : ChampionType.Desgama;
            return new Token(token.GetHashCode(), "Player", "default_room" + token.GetHashCode(), a, SideWars.Shared.Packets.EntityTeam.Blue);
        }
    }
}
