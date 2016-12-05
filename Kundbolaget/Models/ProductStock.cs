using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Kundbolaget.Models
{
    public class ProductStock
    {
        public int Id { get; set; }
        [ForeignKey("ProductInfoId")]
        public virtual ProductInfo ProductInfo { get; set; }
        public int ProductInfoId { get; set; }

    }
}