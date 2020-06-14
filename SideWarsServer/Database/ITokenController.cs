using SideWarsServer.Database.Models;
using System.Threading.Tasks;

namespace SideWarsServer.Database
{
    public interface ITokenController
    {
        Task<Token> GetTokenAsync(string token);
    }
}
