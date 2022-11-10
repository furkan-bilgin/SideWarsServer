using SideWars.Shared.Game;
using SideWars.Shared.Packets;
using SideWarsServer.Database.Models;
using System.Threading.Tasks;

namespace SideWarsServer.Database
{
    public class APITokenController : ITokenController
    {
        public async Task<Token> GetTokenAsync(string token)
        {
            var res = await Server.Instance.APIController.ConfirmUserMatch(token);

            return new Token(res.UserMatchID, res.Username, res.RoomID, (ChampionType)res.UserChampion, (EntityTeam)res.TeamID);
        }
    }
}
