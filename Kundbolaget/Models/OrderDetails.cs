using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Kundbolaget.Models
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Amount { get; set; }
        //public double TotalPrice { get; set; } Nödvändigt?
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