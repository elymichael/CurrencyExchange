namespace CurrencyExchange.Entities
{
    public interface IRateTransactionRepository: IRepository<RateTransaction>
    {
        bool IsUserRegistered(string userID);
        double GetMaxRateSupported(string userID, string code, string month);
    }
}
