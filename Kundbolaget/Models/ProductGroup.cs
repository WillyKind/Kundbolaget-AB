using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kundbolaget.Models
{
    public class ProductGroup
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}