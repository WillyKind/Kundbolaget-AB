using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.OrderUtility
{
    public static class ProductAllocater
    {
        public static void ProductsToOrder(List<Order> orders)
        {
            var orderDetails = orders.SelectMany(x => x.OrderDetails);
            foreach (var detail in orderDetails)
            {
                foreach (var stock in detail.ProductInfo.ProductStocks)
                {
                    if (stock.Amount >= detail.Amount)
                    {
                        detail.ReservedAmount = detail.Amount;
                        stock.Amount -= detail.Amount;
                    }
                    else if (stock.Amount >= 0 && detail.Amount > stock.Amount)
                    {
                        detail.ReservedAmount = stock.Amount;
                        stock.Amount = 0;
                    }
                }
            }
        }

        public static void IsOrderComplete(List<Order> orders)
        {
            foreach (var order in orders)
            {
                order.OrderComplete = order.OrderDetails.All(x => x.Amount == x.ReservedAmount);
            }
        }
    }
}