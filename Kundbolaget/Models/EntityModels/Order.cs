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

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        [Required]
        public int CompanyId { get; set; }
        [Required]
        public bool IsRemoved { get; set; }
    }
}