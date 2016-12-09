using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kundbolaget.Models.EntityModels
{
    public class ProductStock
    {
        public int Id { get; set; }

        [ForeignKey("ProductInfoId")]
        public virtual ProductInfo ProductInfo { get; set; }

        [Required]
        public int Amount { get; set; }


        [Required]
        public int ProductInfoId { get; set; }

        public int WarehouseId { get; set; }
        [ForeignKey("WarehouseId")]

        public virtual Warehouse Warehouse { get; set; }

        [Required]
        public bool IsRemoved { get; set; }
    }
}