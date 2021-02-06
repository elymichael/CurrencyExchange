using CurrencyExchange.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyExchange.Repository
{
    public class CurrencyRepository: Repository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(CurrencyDBContext context) : base(context) { }
    }
}
