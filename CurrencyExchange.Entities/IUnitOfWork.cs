
namespace CurrencyExchange.Entities
{
    using System;
    public interface IUnitOfWork: IDisposable
    {
        ICurrencyRepository Currencies { get; }
        IRateTransactionRepository RateTransactions { get; }
        int Complete();
    }
}
