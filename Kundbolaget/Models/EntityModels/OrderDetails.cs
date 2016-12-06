using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kundbolaget.Models.EntityModels
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Amount { get; set; }

        [ForeignKey("ProductInfoId")]
        public virtual ProductInfo ProductInfo { get; set; }

        [Required]
        public int ProductInfoId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }
}