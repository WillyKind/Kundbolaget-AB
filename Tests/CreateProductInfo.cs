using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.EntityFramework.Repositories;
using Kundbolaget.Models.EntityModels;
using NUnit.Framework;

namespace Tests
{
    class CreateProductInfo
    {
        [TestFixture]
        public class ReadProductsInfo
        {

            [Test]
            public void Create_Product_Info()
            {
                //Instance for testing "GetEntity"
                DbProductInfoRepository repo = new DbProductInfoRepository();

                ProductInfo result;
                ProductInfo expected;

                //Testobject
                #region 

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

                #endregion

                using (var db = new StoreContext())
                {
                    //Saving test-object to DB
                    repo.CreateEntity(pi);
                    //Creating objects to be compared
                    result = db.ProductsInfoes.SingleOrDefault(p => p.Id == pi.Id);
                    expected = pi;
                    //Removes entries in DB.
                    db.ProductsInfoes.Remove(db.ProductsInfoes.FirstOrDefault(p => p.Id == pi.Id));
                    db.ProductGroups.Remove(db.ProductGroups.FirstOrDefault(p => p.Id == pi.ProductGroupId));
                    db.Containers.Remove(db.Containers.FirstOrDefault(p => p.Id == pi.ContainerId));
                    db.SaveChanges();
                }
                //Esuring that Id's are equal 
                Assert.AreEqual(expected.Id, result.Id);
            }
        }
    }
}

