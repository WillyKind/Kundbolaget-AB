using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.ViewModels
{
    public class OrderDetailsViewModel
    {
        public OrderDetails OrderDetails { get; set; }
        public ProductInfo[] ProductInfos { get; set; }
        public OrderDetails[] OrderDetailses { get; set; }
        public int OrderId { get; set; }
        public OrderDetailsViewModel()
        {
            OrderDetails = new OrderDetails();
        }
    }
}   