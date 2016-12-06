using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.Interfaces;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.EntityFramework.Repositories
{
    public class DbProductInfoRepository : IEntityRepository<ProductInfo>
    {
        public ProductInfo[] GetEnity()
        {
            using (var db = new StoreContext())
            {
                return db.ProductsInfoes.ToArray();
            }
        }
    }
}