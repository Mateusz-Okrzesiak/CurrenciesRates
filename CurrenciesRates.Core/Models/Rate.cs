using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrenciesRates.Core.Models
{
    public class Rate
    {
        public string Currency { get; set; }
        public string Code { get; set; }
        public double Mid { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
