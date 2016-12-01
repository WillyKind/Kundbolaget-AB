using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kundbolaget.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int ContactPersonId { get; set; }
        public int CountryId { get; set; }
        public string Email { get; set; }
    }
}