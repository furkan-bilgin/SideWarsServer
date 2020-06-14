using SideWarsServer.Database.Models;
using System.Threading.Tasks;

namespace SideWarsServer.Database
{
    public class DebugTokenController : ITokenController
    {
        public async Task<Token> GetTokenAsync(string token)
        {
            return new Token(true, "Player", token);
        }
    }
}
