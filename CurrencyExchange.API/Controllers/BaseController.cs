namespace CurrencyExchange.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CurrencyExchange.API.model;
    using CurrencyExchange.API.services;
    using CurrencyExchange.Entities;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    public abstract class BaseController : ControllerBase
    {
        protected Configuration _configuration;
        protected readonly IUnitOfWork _unitOfWork;

        public BaseController(
            IOptions<Configuration> configurationAccesor,
            IUnitOfWork unitOfWork)
        {
            _configuration = configurationAccesor.Value;
            _unitOfWork = unitOfWork;
            MockTransactionsAllowed();
        }

        protected CurrencySetting GetCurrencySetting(string isoCode)
        {
            var currency = _configuration.Currencies.FirstOrDefault(a => a.ISO == isoCode);
            return currency;
        }
        
        protected void SaveError(Exception errorMessage, string methodName)
        {
            // Todo Code
        }

        private void MockTransactionsAllowed()
        {
            RateTransaction data = new RateTransaction
            {
                CurrencyCode = "USD",
                MaxRate = 200,
                UserID = "001",
                Month = "202102"
            };
            _unitOfWork.RateTransactions.Add(data);

            data = new RateTransaction
            {
                CurrencyCode = "BRL",
                MaxRate = 300,
                UserID = "001",
                Month = "202102"
            };
            _unitOfWork.RateTransactions.Add(data);
            _unitOfWork.Complete();
        }
    }
}
