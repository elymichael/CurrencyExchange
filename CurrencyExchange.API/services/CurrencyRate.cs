namespace CurrencyExchange.API.services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CurrencyRate : ICurrencyRate
    {
        public async Task<double> GetRateExchange(IRequest request, string code, string url)
        {
            List<string> result = await request.GetDeserializedResult<List<string>>(url);
            if(result.Count > 0)
            {
                return double.Parse(result[0].ToString());
            }
            return 0;
        }
    }
}
