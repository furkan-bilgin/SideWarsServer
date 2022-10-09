using Newtonsoft.Json;
using SideWarsServer.API.Models;
using SideWarsServer.Database.Models;
using SideWarsServer.Utils;
using SideWarsShared.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SideWarsServer.API
{
    public class APIController
    {
        private const string BASE_URL = "http://localhost:3000/api/v1/server";
        private RestClient restClient;

        public APIController()
        {
            restClient = new RestClient();
            restClient.SetHeaderToken(RestClient.GAME_SERVER_TOKEN_HEADER, "SERVERTOKEN_U9gd9ewWbbJNW4yf");// TODO CHANGE Environment.GetEnvironmentVariable("SERVER_TOKEN"));
        }

        public async Task<ConfirmUserMatchModel> ConfirmUserMatch(string matchToken)
        {
            // SideWarsLobbyServer/app/controllers/match_controller.go
            var res = await restClient
                .AddRequestData("UserMatchToken", matchToken)
                .Post<ConfirmUserMatchModel>(BASE_URL + "/confirm-user-match");

            return res;
        }

        public async Task<bool> FinishUserMatches(List<Token> players, List<Token> winners)
        {
            // SideWarsLobbyServer/app/controllers/match_controller.go
            var res = await restClient
                .AddRequestData("UserMatchIDs", JsonConvert.SerializeObject(players.Select(x => x.ID)))
                .AddRequestData("WinnerMatchIDs", JsonConvert.SerializeObject(winners.Select(x => x.ID)))
                .Post(BASE_URL + "/finish-user-matches");

            return (bool)res["Success"];
        }
    }
}
