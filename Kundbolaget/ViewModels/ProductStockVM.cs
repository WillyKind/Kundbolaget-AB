using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.ViewModels
{
    public class ProductStockVM
    {
        public ProductInfo[] ProductInfos { get; set; }
        public Warehouse[] Warehouses { get; set; }
        public ProductStock ProductStock { get; set; }
    }
}