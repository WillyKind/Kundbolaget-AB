using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Kundbolaget.Models
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
        public int ProductGroupId { get; set; }

        [ForeignKey("ProductGroupId")]
        public virtual ProductGroup ProductGroup { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public double Abv { get; set; }

        [Required]
        public double TradingMargin { get; set; }

        [Required]
        public double PurchasePrice { get; set; }

        [Required]
        public bool Removed { get; set; }
    }
}