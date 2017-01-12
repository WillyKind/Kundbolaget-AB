using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kundbolaget.Models.EntityModels;

namespace Tests
{
    internal static class OrderDetailsResources
    {
        public static List<OrderDetails> DummyOrderDetails => new List<OrderDetails>
        {
            new OrderDetails
            {
                Id = 1,
                ProductInfo = ResourceData.ProductInfoList[1],
                ProductInfoId = ResourceData.ProductInfoList[1].Id,
                Amount = 10,
                Order = OrderResourcesData.DummyOrder[0],
                OrderId = OrderResourcesData.DummyOrder[0].Id,
                UnitPrice = ResourceData.ProductInfoList[1].Price,
                TotalPrice = ResourceData.ProductInfoList[1].Price*10,
                ReservedAmount = 11
            },
            new OrderDetails
            {
                Id = 2,
                ProductInfo = ResourceData.ProductInfoList[1],
                ProductInfoId = ResourceData.ProductInfoList[1].Id,
                Amount = 25,
                UnitPrice = ResourceData.ProductInfoList[1].Price,
                TotalPrice = ResourceData.ProductInfoList[1].Price*25,
                Order = OrderResourcesData.DummyOrder[0],
                OrderId = OrderResourcesData.DummyOrder[0].Id,
                ReservedAmount = 25
            }
        };
    }
}