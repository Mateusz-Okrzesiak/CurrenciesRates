using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CurrenciesRates.Core.Models
{
    public class Rate
    {
        [Display(Name = "Nazwa waluty")]
        public string Currency { get; set; }
        [Display(Name = "Symbol")]
        public string Code { get; set; }
        [Display(Name = "Kurs")]
        public double Mid { get; set; }
        [Display(Name = "Kurs z dnia")]
        public DateTime EffectiveDate { get; set; }
    }
}
