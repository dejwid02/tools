using System.Net.Http;
using System.Threading.Tasks;

namespace Services
{
    public interface IApiClient
    {
        Task<TOut> GetAsync<TOut>(string url) where TOut : class;
        Task<TOut> PostAsync<TOut>(string path, HttpContent content);
        Task<TOut> PostAsync<TIn, TOut>(string path, TIn content);
        Task<bool> PutAsync<Tin>(string path, Tin content);
        Task Delete(string path);
    }
}