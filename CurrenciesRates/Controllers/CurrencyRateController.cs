using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ClosedXML.Excel;
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

        public ActionResult CurrenciesRatesList()
        {
            var rates = this._currencyRateService.GetCurrenciesRates();
          
            return View(rates);
        }

        public IEnumerable<double> MonthsAVG(string currencyCode)
        {
            var monthsAvg = this._currencyRateService.GetMonthsAVGCurrency(currencyCode);
            return monthsAvg;
        }
        public  IActionResult GenerateExcelFile(string[] labels, string[] rate)
        {
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "KursWalut.xlsx";
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet =
                    workbook.Worksheets.Add($"Kursy walut");

                    for (int i = 0; i < labels.Length; i++)
                    {
                        worksheet.Cell(1, i + 1).Value = labels[i];
                        worksheet.Cell(2, i + 1).Value = rate[i];
                    }

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);

                        stream.Position = 0;
                        var content = stream.ToArray();
                        return File(content, contentType, fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
