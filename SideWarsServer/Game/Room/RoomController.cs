using SideWarsServer.Database;
using SideWarsServer.Database.Exceptions;
using SideWarsServer.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideWarsServer.Game.Room
{
    public class RoomController
    {
        private ITokenController tokenController;
    
        public RoomController()
        {
            tokenController = new DebugTokenController();
        }

        public async Task<bool> PlayerJoinRequest(string tokenId)
        {
            try 
            {
                var token = await tokenController.GetTokenAsync(tokenId);
                JoinOrCreateRoom(token);

                return true;
            }
            catch(InvalidTokenException ex)
            {
                return false;
            }
        }

        public void JoinOrCreateRoom(Token token)
        {

        }
    }
}
