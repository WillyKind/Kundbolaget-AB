using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kundbolaget.EntityFramework.Repositories;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.OrderUtility
{
    public static class ProductAllocater
    {
        static DbProductStockRepository productStock = new DbProductStockRepository();
        static DbOrderRepository ordersRepository = new DbOrderRepository();
    }
}