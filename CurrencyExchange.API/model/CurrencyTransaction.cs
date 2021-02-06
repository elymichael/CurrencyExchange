namespace CurrencyExchange.API.model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    public class CurrencyTransaction
    {
        [Required]
        public string UserID { get; set; }
        [Required]       
        public double Amount { get; set; }
        [Required]
        public string CurrencyCode { get; set; }
    }
}
