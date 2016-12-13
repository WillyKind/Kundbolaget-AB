using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var productInfoList = ProductInfoList();
            var mock = new Mock<DbSet<ProductInfo>>();
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