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

        public Order Order { get; set; }

        [Required]
        public double PriceWithCompanyDiscount { get; set; }

        [Required]
        public double OriginalPrice { get; set; }

        [Required]
        public double PriceWithPalletDiscount { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public int CustomerOrderId { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public bool IsRemoved { get; set; }
    }
}