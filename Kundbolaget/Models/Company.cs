using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kundbolaget.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int AddressId { get; set; }
        public int ContactPersonId { get; set; }
        public int CountryId { get; set; }
        public int DeliveryAddressId { get; set; }
        public int GroupId { get; set; }
    }
}