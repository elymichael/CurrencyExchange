namespace CurrencyExchange.Repository
{
    using CurrencyExchange.Entities;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CurrencyDBContext _context;
        public ICurrencyRepository Currencies { get; }
        public IRateTransactionRepository RateTransactions { get; }
        public UnitOfWork(CurrencyDBContext currencyDBContext, 
            ICurrencyRepository currencyRepository, 
            IRateTransactionRepository transactionsRepository)
        {
            this._context = currencyDBContext;
            this.Currencies = currencyRepository;
            this.RateTransactions = transactionsRepository;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
