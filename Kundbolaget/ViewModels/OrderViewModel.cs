using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.ViewModels
{
    public class OrderViewModel
    {
        public Company[] Companies { get; set; }
        public Order Order { get; set; }     
    }
}