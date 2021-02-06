namespace CurrencyExchange.Repository
{
    using CurrencyExchange.Entities;
    using System.Linq;

    public class RateTransactionRepository: Repository<RateTransaction>, IRateTransactionRepository
    {
        public RateTransactionRepository(CurrencyDBContext context) : base(context) { }

        public bool IsUserRegistered(string userID)
        {
            var data = _context.Transactions.FirstOrDefault(x => x.UserID == userID);
            if (data != null) { return true; };

            return false;
        }

        public double GetMaxRateSupported(string userID, string code, string month)
        {
            var data = _context.Transactions.FirstOrDefault(x => x.UserID == userID && x.CurrencyCode == code && x.Month == month);
            if (data != null) { return data.MaxRate; };

            return -1;
        }
    }
}
