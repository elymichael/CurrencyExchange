namespace CurrencyExchange.API.services
{
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    public class Request : IRequest
    {
        private HttpClient client;
        public Request()
        {
            client = new HttpClient();
        }

        public async Task<T> GetDeserializedResult<T>(string url)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            var jsonResult = client.GetStreamAsync(url);

            var result = await JsonSerializer.DeserializeAsync<T>(await jsonResult);

            return result;
        }

        public async Task<string> GetStringResult(string url)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            var result = await client.GetStringAsync(url);
            return result;
        }
    }
}
