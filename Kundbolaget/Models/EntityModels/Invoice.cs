using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kundbolaget.Models.EntityModels
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        public int Orderid { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public int CustomerOrderId { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public bool IsRemoved { get; set; }
    }
}