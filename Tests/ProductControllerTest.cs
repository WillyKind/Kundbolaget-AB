using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.Models.EntityModels;
using Moq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    class ProductControllerTest
    {
        [SetUp]
        public void Initializer() {}

        [Test]
        public void TestTest()
        {
            var data = ProductInfoList().AsQueryable();
            var mockSet = new Mock<DbSet<ProductInfo>>();

            mockSet.As<IQueryable<ProductInfo>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<ProductInfo>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<ProductInfo>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<ProductInfo>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = new Mock<StoreContext>();
            mockContext.Setup(x => x.ProductsInfoes).Returns(mockSet.Object);


        }

        private static List<ProductInfo> ProductInfoList()
        {
            var category = new Category
            {
                Id = 1,
                Name = "Öl",
            };
            var data = new List<ProductInfo>
            {
                new ProductInfo
                {
                    Id = 1,
                    Container = new Container
                    {
                        Name = "Flaska"
                    },
                    Volume = new Volume
                    {
                        Milliliter = 330
                    },
                    ProductGroup = new ProductGroup
                    {
                        Id = 1,
                        Category = category,
                        Name = "Ale",
                    },
                    Abv = 7,
                    Name = "Kalas Oscars finöl",
                    PurchasePrice = 50,
                    TradingMargin = 15,
                    Description = "Kalas ska det vara."
                },
                new ProductInfo
                {
                    Id = 2,
                    ProductGroup = new ProductGroup
                    {
                        Id = 2,
                        Category = category,
                        Name = "Lager",
                    },
                    Name = "Sofiero",
                    Abv = 8,
                    PurchasePrice = 30,
                    TradingMargin = 10,
                    Container = new Container
                    {
                        Name = "Burk",
                    },
                    Volume = new Volume
                    {
                        Milliliter = 500
                    },
                    Description = "Sofieros fina goda öl."
                }
            };
            return data;
        }
    }
}