using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CurrenciesRates.Core.Models;
using CurrenciesRates.Models;
using Newtonsoft.Json.Linq;

namespace CurrenciesRates.Core.CurrencyRate
{
    public interface ICurrencyRateService
    {
        Rate GetCurrentRateByCode(string code);
        IEnumerable<Rate> GetCurrenciesRates();
        IEnumerable<Rate> GetCurrencyFromRange(DateTime startDate, DateTime endDate, string currencyCode);
        IEnumerable<double> GetMonthsAVGCurrency(string currencyCode);
    }

    public class CurrencyRateService : ICurrencyRateService
    {
        private readonly INBPService _nbpService;

        public CurrencyRateService(INBPService nbpService)
        {
            this._nbpService = nbpService;
        }

        public Rate GetCurrentRateByCode(string currencyCode)
        {
            var result = this._nbpService.CreateWebRequest(_nbpService.Api_Url + $"rates/a/{currencyCode}/?format=json");
            Rate rate = null;

            if (!string.IsNullOrWhiteSpace(result))
            {
                var root = JObject.Parse(result);
                rate = root["rates"][0].ToObject<Rate>();
                rate.Code = root["code"].ToString();
                rate.Currency = root["currency"].ToString();
            }
            return rate;
        }
        public IEnumerable<Rate> GetCurrenciesRates()
        {
            var result = this._nbpService.CreateWebRequest(_nbpService.Api_Url + "/tables/a/?format=json");
            IEnumerable<Rate> rates = null;

            if (!string.IsNullOrWhiteSpace(result))
            {
                var root = JArray.Parse(result);
                rates = root[0]["rates"].ToObject<IEnumerable<Rate>>();
            }
            return rates;
        }

        public IEnumerable<Rate> GetCurrencyFromRange(DateTime startDate, DateTime endDate, string currencyCode)
        {
            var result = this._nbpService.CreateWebRequest(_nbpService.Api_Url + $"rates/a/{ currencyCode }/{ startDate.ToString("yyyy-MM-dd") }/{ endDate.ToString("yyyy-MM-dd") }/?format=json");
            IEnumerable<Rate> rates = null;

            if (!string.IsNullOrWhiteSpace(result))
            {
                var root = JObject.Parse(result);
                rates = root["rates"].ToObject<IEnumerable<Rate>>();
            }
            return rates;
        }

        public IEnumerable<double> GetMonthsAVGCurrency(string currencyCode)
        {
            var result = GetCurrencyFromRange(new DateTime(DateTime.Now.Year, 1, 1), DateTime.Now, currencyCode).GroupBy(x => x.EffectiveDate.Month)
                                             .Select(group => new { AVG = group.Sum(x => x.Mid) / group.Count() }).Select(a => a.AVG);

            return result;     
        }
    }
} 
