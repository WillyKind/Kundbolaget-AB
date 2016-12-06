using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kundbolaget.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}