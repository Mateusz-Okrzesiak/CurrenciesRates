using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrenciesRates.Models
{
    public class Currency
    {
        public string Name { get; set; }
        public string CurrencySymbol { get; set; }
        public double AVGRate { get; set; }
    }
}
