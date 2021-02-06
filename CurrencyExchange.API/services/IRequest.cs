namespace CurrencyExchange.API.services
{
    using System.Threading.Tasks;
    public interface IRequest
    {
        Task<string> GetStringResult(string url);
        Task<T> GetDeserializedResult<T>(string url);
    }
}
