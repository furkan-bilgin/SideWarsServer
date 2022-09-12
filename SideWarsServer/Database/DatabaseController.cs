using SideWarsServer.Database.Models;
using SideWarsServer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideWarsServer.Database
{
    public class DatabaseController
    {
        private ITokenController tokenController;

        public DatabaseController(ITokenController tokenController)
        {
            this.tokenController = tokenController;
        }

        public async Task<Token> GetTokenAsync(string tokenId)
        {
            try
            {
                var token = await tokenController.GetTokenAsync(tokenId);
                return token;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }
    }
}
