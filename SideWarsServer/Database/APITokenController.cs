using SideWars.Shared.Game;
using SideWarsServer.API;
using SideWarsServer.Database.Models;
using System.Threading.Tasks;

namespace SideWarsServer.Database
{
    public class APITokenController : ITokenController
    {
        public async Task<Token> GetTokenAsync(string token)
        {
            var res = await Server.Instance.APIController.ConfirmUserMatch(token);

            return new Token(res.UserID, res.Username, res.RoomID, (ChampionType)res.UserChampion);
        }
    }
}
