using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kundbolaget.Models.EntityModels
{
    public class InvoiceDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int InvoiceId { get; set; }

        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }

        [Required]
        public double FinalPrice { get; set; }

        [ForeignKey("ProductInfoId")]
        public ProductInfo ProductInfo { get; set; }

        [Required]
        public int ProductInfoId { get; set; }
    }
}