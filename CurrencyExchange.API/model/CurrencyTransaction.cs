namespace CurrencyExchange.API.model
{
    using System.ComponentModel.DataAnnotations;
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
