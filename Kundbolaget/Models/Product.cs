using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kundbolaget.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int ContainerId { get; set; }
        public int ProductGroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Abv { get; set; }
        public double TradingMargin { get; set; }
        public double PurchasePrice { get; set; }
        public bool Removed { get; set; }
    }
}