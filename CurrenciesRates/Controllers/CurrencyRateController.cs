using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Serialization;
using CurrenciesRates.Core.CurrencyRate;
using CurrenciesRates.Core.Models;
using CurrenciesRates.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CurrenciesRates.Controllers
{
    public class CurrencyRateController : Controller
    {

        private readonly ICurrencyRateService _currencyRateService;


        public CurrencyRateController(ICurrencyRateService currencyRateService)
        {
            _currencyRateService = currencyRateService;
        }
        // GET: CurrencyRate
        public ActionResult Index()
        {
            return View();
        }

        // GET: CurrencyRate/Details/5
        public ActionResult Details(string currencyCode)
        {
            DateTime date = DateTime.Now;
            CurrencyViewModel cvm = new CurrencyViewModel();

            cvm.CurrentRate = this._currencyRateService.GetCurrentRateByCode(currencyCode);
            cvm.RangeCurrencyRate = this._currencyRateService.GetCurrencyFromRange(new DateTime(date.Year, date.Month, 1), date, currencyCode);

            return View(cvm);
        }

        // GET: CurrencyRate/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CurrencyRate/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CurrencyRate/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CurrencyRate/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CurrencyRate/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CurrencyRate/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CurrenciesRatesList()
        {

            var rates = this._currencyRateService.GetCurrenciesRates();
          
            return View(rates);
        }
    }
}
