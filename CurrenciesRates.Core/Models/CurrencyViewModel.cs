using CurrenciesRates.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrenciesRates.Core.Models
{
    public class CurrencyViewModel
    {
        public Rate CurrentRate { get; set; }
        public IEnumerable<Rate> RangeCurrencyRate { get; set; }
    }
}
