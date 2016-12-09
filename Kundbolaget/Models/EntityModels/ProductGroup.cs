using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kundbolaget.Models.EntityModels
{
    public class ProductGroup
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public virtual Category Category { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<ProductInfo> ProductInfoes { get; set; }
        [Required]
        public bool IsRemoved { get; set; }
    }
}