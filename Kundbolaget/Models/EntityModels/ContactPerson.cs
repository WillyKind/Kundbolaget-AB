﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kundbolaget.Models.EntityModels
{
    public class ContactPerson
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        public ICollection<Company> Company { get; set; }

        public virtual ICollection<Warehouse> Warehouses { get; set; }
        [Required]
        public bool IsRemoved { get; set; }
    }
}