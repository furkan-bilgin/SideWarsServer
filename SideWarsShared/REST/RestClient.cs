using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SideWarsShared.REST
{
    public class APIErrorException : Exception
    {
        public APIErrorException(string message) : base(message)
        {
        }
    }

    public class RestClient
    {
        public const string GAME_SERVER_TOKEN_HEADER = "SW-ServerToken";
        public const string CLIENT_TOKEN_HEADER = "SW-ClientToken";

        private WebClient webClient = new WebClient();
        private NameValueCollection requestData = new NameValueCollection();

        public void SetHeaderToken(string header, string token)
        {
            webClient.Headers[header] = token;
        }

        public RestClient AddRequestData(string key, string val)
        {
            requestData.Add(key, val);
            return this;
        }

        public async Task<T> Post<T>(string url, bool checkAPIError = true) where T : class
        {
            var respBuffer = await webClient.UploadValuesTaskAsync(new Uri(url), requestData);
            var resp = Encoding.UTF8.GetString(respBuffer);

            requestData.Clear();

            var jsonDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(resp);
            if (checkAPIError)
                CheckAPIError(jsonDict);

            return JsonConvert.DeserializeObject<T>(resp);
        }

        public async Task<Dictionary<string, object>> Post(string url, bool checkAPIError = true)
        {
            return await Post<Dictionary<string, object>>(url, checkAPIError);
        }

        public async Task<T> Get<T>(string url, bool checkAPIError = true) where T : class
        {
            var resp = await webClient.DownloadStringTaskAsync(new Uri(url));

            var jsonDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(resp); 
            if (checkAPIError)
                CheckAPIError(jsonDict);

            return JsonConvert.DeserializeObject<T>(resp);
        }

        public async Task<byte[]> GetRaw(string url)
        {
            return await webClient.DownloadDataTaskAsync(url);
        }

        private void CheckAPIError(Dictionary<string, object> json)
        {
            if (json.ContainsKey("Error"))
                throw new APIErrorException(json["Message"].ToString());
        }
    }
}
