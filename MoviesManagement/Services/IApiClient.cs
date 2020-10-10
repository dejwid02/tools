using System.Threading.Tasks;

namespace Services
{
    public interface IApiClient
    {
        Task<TOut> Get<TOut>(string url) where TOut : class;
    }
}