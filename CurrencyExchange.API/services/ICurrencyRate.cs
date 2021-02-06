namespace CurrencyExchange.API.services
{
    using System.Threading.Tasks;
    public interface ICurrencyRate
    {
        Task<double> GetRateExchange(IRequest request, string code, string url);
    }
}
