using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kundbolaget.Models.EntityModels
{
    public class ProductInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ContainerId { get; set; }

        [ForeignKey("ContainerId")]
        public virtual Container Container { get; set; }

        [Required]
        public int VolumeId { get; set; }

        [ForeignKey("VolumeId")]
        public virtual Volume Volume { get; set; }

        [Required]
        public int ProductGroupId { get; set; }

        [ForeignKey("ProductGroupId")]
        public virtual ProductGroup ProductGroup { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Abv { get; set; }

        [Required]
        public double TradingMargin { get; set; }

        [Required]
        public decimal PurchasePrice { get; set; }

        [Required]
        public bool IsRemoved { get; set; }

        [Required]
        public decimal Price { get; set; }

        public decimal? NewPrice { get; set; }

        public DateTime? PriceStart { get; set; }


        public decimal? PalletDiscount { get; set; }

        public virtual ICollection<ProductStock> ProductStocks { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
