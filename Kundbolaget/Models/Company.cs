using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Kundbolaget.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public int AddressId { get; set; }
        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }
        [Required]
        public int ContactPersonId { get; set; }
        [ForeignKey("ContactPersonId")]
        public virtual ContactPerson ContactPerson { get; set; }
        public int CountryId { get; set; }
        public int DeliveryAddressId { get; set; }
        public int GroupId { get; set; }
    }
}