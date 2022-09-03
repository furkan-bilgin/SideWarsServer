using Newtonsoft.Json;
using System;
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

            dynamic json = JsonConvert.DeserializeObject(resp);
            if (checkAPIError)
                json = CheckAPIError(json);

            return (T)json;
        }

        public async Task<T> Get<T>(string url, bool checkAPIError = true) where T : class
        {
            var resp = await webClient.DownloadStringTaskAsync(new Uri(url));

            dynamic json = JsonConvert.DeserializeObject(resp); 
            if (checkAPIError)
                json = CheckAPIError(json);

            return (T)json;
        }

        public async Task<byte[]> GetRaw(string url)
        {
            return await webClient.DownloadDataTaskAsync(url);
        }

        private dynamic CheckAPIError(dynamic json)
        {
            string errorMessage;
            try
            {
                var error = json.Error;
                // There is error, so let's throw an exception
                errorMessage = error.Message;
            }
            catch
            {
                // No error, we are good
                return json;
            }

            throw new APIErrorException(errorMessage);
        }
    }
}
