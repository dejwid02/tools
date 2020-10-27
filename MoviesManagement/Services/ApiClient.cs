using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
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

        public async Task<TOut> PostAsync<TOut>(string path, HttpContent content)
        {
            var result = await client.PostAsync(path, content);
            int status = (int)(result.StatusCode);

            var resultString = await (result.Content?.ReadAsStringAsync() ?? Task.FromResult(default(string)));
            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TOut>(resultString);
            }
            HandleError(status);
            return default(TOut);
        }

        public async Task<TOut> PostAsync<TIn, TOut>(string path, TIn content)
        {
            var json = JsonConvert.SerializeObject(content);
            var sContent = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await client.PostAsync(path, sContent);
            int status = (int)(result.StatusCode);

            var resultString = await (result.Content?.ReadAsStringAsync() ?? Task.FromResult(default(string)));
            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TOut>(resultString);
            }
            HandleError(status);
            return default(TOut);
        }

        public static void HandleError(int statusCode)
        {
            throw new InvalidOperationException($"Connection with api failed with code{statusCode}");
        }

        public async Task Delete(string path)
        {
            var result = await client.DeleteAsync(path);
            if (result.IsSuccessStatusCode)
            {
                return;
            }
            HandleError((int)result.StatusCode);
        }
    }
}
