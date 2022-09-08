using SideWarsServer.API.Models;
using SideWarsServer.Utils;
using SideWarsShared.REST;
using System;
using System.Threading.Tasks;

namespace SideWarsServer.API
{
    public class APIController : Singleton<APIController>
    {
        private const string BASE_URL = "http://192.168.1.69:3000/api/v1/server";
        private RestClient restClient;

        public APIController()
        {
            restClient = new RestClient();
            restClient.SetHeaderToken(RestClient.GAME_SERVER_TOKEN_HEADER, Environment.GetEnvironmentVariable("SERVER_TOKEN"));

            InitSingleton(this);
        }

        public async Task<ConfirmUserMatchModel> ConfirmUserMatch(string matchToken)
        {
            // SideWarsLobbyServer/app/controllers/match_controller.go
            var res = await restClient
                .AddRequestData("UserMatchToken", matchToken)
                .Post<ConfirmUserMatchModel>(BASE_URL + "/confirm-user-match");

            return res;
        }
    }
}
