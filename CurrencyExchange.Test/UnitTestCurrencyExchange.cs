namespace CurrencyExchange.Test
{
    using CurrencyExchange.API.Controllers;
    using CurrencyExchange.API.model;
    using CurrencyExchange.API.services;
    using CurrencyExchange.Entities;
    using CurrencyExchange.Repository;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using NUnit.Framework;

    public class Tests
    {
        private ICurrencyRate currencyRate;
        private IRequest request;
        private ICurrencyRepository currencyRepository;
        private IRateTransactionRepository rateTransactionRepository;
        private IUnitOfWork unitOfWork;

        private IOptions<Configuration> configurationAccessor;
        private CurrencyDBContext currencyDBContext;
        private DefaultController defaulController;

        [SetUp]
        public void Setup()
        {
            configurationAccessor = Options.Create<Configuration>(new Configuration()
            {
                Currencies = new System.Collections.Generic.List<CurrencySetting>()
                {
                    new CurrencySetting { ISO = "USD", ApiUrl = "http://www.bancoprovincia.com.ar/Principal/Dolar", FakeValue = 1, MaxSupported = 200 },
                    new CurrencySetting { ISO = "BRL", ApiUrl = "", FakeValue = 1, MaxSupported = 300 }
                }
            });

            var options = new DbContextOptionsBuilder<CurrencyDBContext>().UseInMemoryDatabase("ServiceTest").Options;

            currencyDBContext = new CurrencyDBContext(options);

            currencyRepository = new CurrencyRepository(currencyDBContext);
            rateTransactionRepository = new RateTransactionRepository(currencyDBContext);

            unitOfWork = new UnitOfWork(currencyDBContext, currencyRepository, rateTransactionRepository);
            currencyRate = new CurrencyRate();
            request = new Request();

            defaulController = new DefaultController(configurationAccessor, request, currencyRate, unitOfWork);
        }

        [Test]
        public void ValidateRightCallExchangeRateUSD()
        {
            var result = defaulController.GetExchangeRate("USD");
            Assert.IsTrue(result.Value > 0);
        }

        [Test]
        public void ValidateRightCallExchangeRateBRL()
        {
            var result = defaulController.GetExchangeRate("BRL");
            Assert.IsTrue(result.Value > 0);
        }

        [Test]
        public void GetCurrencyRateForCodeNotSupported()
        {
            var result = defaulController.GetExchangeRate("DOM");
            var actual = result.Result as BadRequestObjectResult;
            Assert.IsNotNull(actual);
        }

        [Test]
        public void GetInvalidUserForCurrencyExchange()
        {
            CurrencyTransaction data = new CurrencyTransaction
            {
                Amount = 100,
                CurrencyCode = "USD",
                UserID = "002"                
            };

            var result = defaulController.Save(data);
            var actual = result as BadRequestObjectResult;
            Assert.AreEqual("Invalid UserID", actual.Value);
        }

        [Test]
        public void GetOverLimitCurrencyExchange()
        {
            CurrencyTransaction data = new CurrencyTransaction
            {
                Amount = 205,
                CurrencyCode = "USD",
                UserID = "001"
            };

            var result = defaulController.Save(data);
            var actual = result as BadRequestObjectResult;
            Assert.AreEqual("The limit in USD is 200.", actual.Value);
        }

        [Test]
        public void GetValidLimitCurrencyExchange()
        {
            CurrencyTransaction data = new CurrencyTransaction
            {
                Amount = 199,
                CurrencyCode = "USD",
                UserID = "001"
            };

            var result = defaulController.Save(data);
            var actual = result as OkResult;
            Assert.IsNotNull(actual);
        }
    }
}