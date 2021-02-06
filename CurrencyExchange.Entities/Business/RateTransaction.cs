namespace CurrencyExchange.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class RateTransaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        public double MaxRate { get; set; }
        [Required]
        public string CurrencyCode { get; set; }
        [Required]
        public string Month { get; set; }
    }
}
