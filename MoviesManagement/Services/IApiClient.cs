using System.Net.Http;
using System.Threading.Tasks;

namespace Services
{
    public interface IApiClient
    {
        Task<TOut> Get<TOut>(string url) where TOut : class;

        Task<TOut> PostAsync<TOut> (string path, HttpContent content)

    }
}