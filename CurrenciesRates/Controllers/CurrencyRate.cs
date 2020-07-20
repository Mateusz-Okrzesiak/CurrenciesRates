using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrenciesRates.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurrenciesRates.Controllers
{
    public class CurrencyRate : Controller
    {
        // GET: CurrencyRate
        public ActionResult Index()
        {
            return View();
        }

        // GET: CurrencyRate/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
                Repository.AddResponse();
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
            if(!Repository.Currencies.Any())
                Repository.AddResponse();

            return View(Repository.Currencies);
        }
    }
}
