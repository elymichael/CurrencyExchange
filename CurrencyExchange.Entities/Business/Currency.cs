namespace CurrencyExchange.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Currency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public string CurrencyCode { get; set; }
        public double Rate { get; set; }
        public double Total { get; set; }
    }
}
