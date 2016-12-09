using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kundbolaget.Models.EntityModels
{
    public class Container
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<ProductInfo> ProductsInfoes { get; set; }
        [Required]
        public bool IsRemoved { get; set; }
    }

    public class Volume
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public double Milliliter { get; set; }
        public virtual ICollection<ProductInfo> ProductInfos { get; set; }
        [Required]
        public bool IsRemoved { get; set; }
    }
}