using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kundbolaget.Models
{
    public class ContactPerson
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}