namespace CurrencyExchange.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CurrencyExchange.API.Constants;
    using CurrencyExchange.API.model;
    using CurrencyExchange.API.services;
    using CurrencyExchange.Entities;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : BaseController
    {
        private IRequest _request;
        private ICurrencyRate _currencyRate;
        public DefaultController(
            IOptions<Configuration> accessor,
            IRequest request,
            ICurrencyRate currencyRate,
            IUnitOfWork unitOfWork) : base(accessor, unitOfWork)
        {
            _request = request;
            _currencyRate = currencyRate;
        }

        [HttpGet("{code}")]
        public ActionResult<double> GetExchangeRate(string code)
        {
            double rate = GetCurrencyRate(code);

            if (rate < 0) {
                return BadRequest($"The ISO Code provided [{code}] is not supported for this API.");
            }

            if (rate == 0) {
                return BadRequest("An unexpected error was found in the request.");
            }

            return rate;
        }

        [HttpPost]
        public ActionResult Save(CurrencyTransaction data)
        {
            if (!ModelState.IsValid) {
                return BadRequest("Some information are invalid");
            }

            if (!_unitOfWork.RateTransactions.IsUserRegistered(data.UserID))
            {
                return BadRequest("Invalid UserID");
            }

            var maxRate = _unitOfWork.RateTransactions.GetMaxRateSupported(data.UserID, data.CurrencyCode, DateTime.Now.ToString("yyyyMM"));
            if(maxRate < 0)
            {
                return BadRequest($"Transaction not supported for this Code {data.CurrencyCode} in this period.");
            }

            if(data.Amount > maxRate) {
                return BadRequest($"The limit in {data.CurrencyCode} is {maxRate}.");
            }

            var currency = GetCurrencySetting(data.CurrencyCode);

            double rate = GetCurrencyRate(data.CurrencyCode);

            if(rate < 0) {
                return BadRequest($"The ISO Code provided [{data.CurrencyCode}] is not supported for this API.");
            }

            if(rate == 0) {
                return BadRequest("An unexpected error was found in the request.");
            }
            
            Currency model = new Currency
            {
                UserID = data.UserID,
                Amount = data.Amount,
                Rate = rate,
                CurrencyCode = data.CurrencyCode,
                Total = data.Amount * rate
            };

            _unitOfWork.Currencies.Add(model);
            _unitOfWork.Complete();
            return Ok();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Currency>> GetAll()
        {
            var result = _unitOfWork.Currencies.GetAll();
            return Ok(result);
        }

        private double GetCurrencyRate(string code)
        {
            try { 
            var currency = GetCurrencySetting(code);
            if (currency == null)
            {
                return -1;
            }

            double fakeValue = currency.FakeValue;
            if (string.IsNullOrEmpty(currency.ApiUrl))
                currency = GetCurrencySetting(ApiConstants.DEFAULT_CODE_ISO);

            var result = _currencyRate.GetRateExchange(_request, code, currency.ApiUrl).Result;
            result /= fakeValue;

            return result;
            }
            catch (Exception ex)
            {
                SaveError(ex, "GetCurrencyRate");
                return 0;
            }
        }
    }
}
