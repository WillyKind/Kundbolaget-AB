using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kundbolaget.Models.EntityModels
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public int CustomerOrderId { get; set; }

        [Required]
        public DateTime WishedDeliveryDate { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public bool IsRemoved { get; set; }

        [Required]
        public double Price { get; set; }

        public DateTime? OrderPicked { get; set; }
        public DateTime? OrderTransported { get; set; }
        public DateTime? OrderDelivered { get; set; }
        public string Comment { get; set; }

        public virtual Invoice Invoice { get; set; }
    }

}