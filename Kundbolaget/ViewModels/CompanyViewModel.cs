using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.ViewModels
{
    public class CompanyViewModel
    {
        public Company Company { get; set; }
        public Address[] Addresses { get; set; }
        public Country[] Countries { get; set; }
        public ContactPerson[] ContactPersons { get; set; }
        public Company[] ParentCompanies { get; set; }
    }
}