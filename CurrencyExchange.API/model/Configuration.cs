namespace CurrencyExchange.API.model
{
    using System.Collections.Generic;
    public class Configuration
    {
        public List<CurrencySetting> Currencies { get; set; }
    }

    public class CurrencySetting
    {
        public string ISO { get; set; }
        public string ApiUrl { get; set; }
        public double FakeValue { get; set; }
        public double MaxSupported { get; set; }
    }
}
