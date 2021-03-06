﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kundbolaget.Models.EntityModels
{
    public class Warehouse
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int AmmountOfStorageSpace { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [ForeignKey("ContactPersonId")]
        public virtual ContactPerson ContactPerson { get; set; }

        [Required]
        public int ContactPersonId { get; set; }

        [Required]
        public int AddressId { get; set; }

        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }

        public virtual ICollection<ProductStock> ProductStocks { get; set; }
        [Required]
        public bool IsRemoved { get; set; }
    }
}