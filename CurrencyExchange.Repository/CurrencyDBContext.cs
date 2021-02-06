namespace CurrencyExchange.Repository
{
    using CurrencyExchange.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class CurrencyDBContext: DbContext
    {
        public CurrencyDBContext(DbContextOptions<CurrencyDBContext> options): base(options) { }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<RateTransaction> Transactions { get; set; }
    }
}
