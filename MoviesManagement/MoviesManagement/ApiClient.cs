using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace MoviesManagement
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient client;
        private readonly IHttpContextAccessor _accessor;

        public ApiClient(HttpClient client, IHttpContextAccessor accessor)
        {
            this.client = client;
            _accessor = accessor;
        }

        public async Task<TOut> GetAsync<TOut>(string url)
            where TOut : class
        {
            var token = await _accessor.HttpContext.GetTokenAsync("access_token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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
            int status = (int)result.StatusCode;

            var resultString = await (result.Content?.ReadAsStringAsync() ?? Task.FromResult(default(string)));
            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TOut>(resultString);
            }
            HandleError(status);
            return default;
        }

        public async Task<TOut> PostAsync<TIn, TOut>(string path, TIn content)
        {
            var json = JsonConvert.SerializeObject(content);
            var sContent = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await client.PostAsync(path, sContent);
            int status = (int)result.StatusCode;

            var resultString = await (result.Content?.ReadAsStringAsync() ?? Task.FromResult(default(string)));
            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TOut>(resultString);
            }
            HandleError(status);
            return default;
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

        public async Task<bool> PutAsync<Tin>(string path, Tin content)
        {
            var json = JsonConvert.SerializeObject(content);
            var content2 = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await client.PutAsync(path, content2);
            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            HandleError((int)result.StatusCode);
            return false;
        }
    }
}
