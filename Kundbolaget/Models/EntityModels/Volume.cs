using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kundbolaget.Models.EntityModels
{
    public class Volume
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Milliliter { get; set; }
        public virtual ICollection<ProductInfo> ProductInfos { get; set; }
        [Required]
        public bool IsRemoved { get; set; }
    }
}