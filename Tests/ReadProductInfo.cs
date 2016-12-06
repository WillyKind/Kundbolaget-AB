using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.EntityFramework.Repositories;
using Kundbolaget.Models.EntityModels;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ReadProductInfo
    {


        [Test]
        public void Read_Product_Info()
        {
            DbProductInfoRepository repo = new DbProductInfoRepository();
            ProductInfo[] result;
            ProductInfo[] expected;
            var container = new Container
            {
                Name = "33cl",
                Id = 99,
                Volume = 33
            };
            var pg = new ProductGroup
            {
                Name = "Stout",
                Category = new Category
                {
                    Name = "Öl"
                }
            };
            var pi = new ProductInfo()
            {
                Name = "Dryck",
                Description = "Mycket stark",
                Abv = 4.3,
                ProductGroup = pg,
                Container = container,
                PurchasePrice = 187,
                TradingMargin = 1.24
            };
            using (var db = new StoreContext())
            {
                db.ProductsInfoes.Add(pi);
                db.SaveChanges();
                result = repo.GetEntities().ToArray();
                expected = db.ProductsInfoes.ToArray();
                db.ProductsInfoes.Remove(pi);
                db.ProductGroups.Remove(pg);
                db.Containers.Remove(container);
                db.SaveChanges();
                
            }
            var enumerable = result.Zip(expected,(info, productInfo) => info.Id == productInfo.Id);
            Assert.IsTrue(enumerable.All(x => x));
        }
    }
}
