using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Kundbolaget.Controllers;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.EntityFramework.Repositories;
using Kundbolaget.Models.EntityModels;
using Moq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    class ProductControllerTest
    {
        private Mock<DbSet<ProductInfo>> _mockSet;
        private ProductController _productController;
        private Mock<StoreContext> _mockContext;


        [SetUp]
        public void Initializer()
        {
            _mockContext = new Mock<StoreContext>();
            _mockSet = new Mock<DbSet<ProductInfo>>();
            var data = ResourceData.ProductInfoList.AsQueryable();

            SetupDb(_mockSet, data);
            _mockContext.Setup(x => x.ProductsInfoes).Returns(_mockSet.Object);
            // Injects mock database.
            var dbProductInfoRepository = new DbProductInfoRepository(_mockContext.Object);
            _productController = new ProductController(dbProductInfoRepository);
        }

        public void SetupDb<T>(Mock<DbSet<T>> mock, IQueryable<T> data) where T : class
        {
            mock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator);
            _mockContext.Setup(x => x.Set<T>()).Returns(mock.Object);
        }


        [Test]
        public void Index_Retrieve_All_Data()
        {
            var actionResult = _productController.Index();
            var viewResult = actionResult as ViewResult;
            var viewResultModel = (ProductInfo[]) viewResult.Model;
            var productInfos = viewResultModel.ToList();
            Assert.AreEqual(2, productInfos.Count);
            Assert.IsTrue(productInfos.All(x => x.ProductGroup.Category.Name == "Öl"));
        }

        [Test]
        public void Delete_Get_Object()
        {
            var actionResult = _productController.Delete(1);
            var viewResult = actionResult as ViewResult;
            var productInfo = (ProductInfo) viewResult.Model;

            Assert.AreEqual(1, productInfo.Id);
        }

        [Test]
        public void Delete_Change_IsRemoved()
        {
            var productInfo = _mockSet.Object.First(x => x.Id == 1);
            _productController.Delete(productInfo, productInfo.Id);
            var result = _mockSet.Object.First(x => x.Id == productInfo.Id);
            Assert.AreEqual(true, result.IsRemoved);
        }

        [Test]
        public void Edit_Get_Object() {}
    }
}