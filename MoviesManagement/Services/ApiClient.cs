using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient client;

        public ApiClient(HttpClient client)
        {
            this.client = client;
        }

        public async Task<TOut> Get<TOut>(string url)
            where TOut : class
        {
            var response = await client.GetAsync(url);
            var status = (int)response.StatusCode;

            var contentString = await (response.Content?.ReadAsStringAsync() ?? Task.FromResult(""));
            if (response.IsSuccessStatusCode)
            {
                var obj = JsonConvert.DeserializeObject<TOut>(contentString);
                return obj;
            }
            HandleError(status);
            return null;
        }

        public static void HandleError(int statusCode)
        {
            throw new InvalidOperationException($"Connection with api failed with code{statusCode}");
        }
    }
}
