using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Castle.Components.DictionaryAdapter.Xml;
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
        private Mock<DbSet<ProductInfo>> _mockSetProductInfo;
        private Mock<DbSet<Container>> _mockSetContainer;
        private Mock<DbSet<ProductGroup>> _mockSetProductGroup;
        private Mock<DbSet<Volume>> _mockSetVolume;


        private ProductController _productController;
        private Mock<StoreContext> _mockContext;


        [SetUp]
        public void Initializer()
        {
            _mockContext = new Mock<StoreContext>();
            _mockSetProductInfo = new Mock<DbSet<ProductInfo>>();
            _mockSetContainer = new Mock<DbSet<Container>>();
            _mockSetProductGroup = new Mock<DbSet<ProductGroup>>();
            _mockSetVolume = new Mock<DbSet<Volume>>();

            var dataProductInfos = ResourceData.ProductInfoList.AsQueryable();
            var dataContainers = ResourceData.Containers.AsQueryable();
            var productGroups = ResourceData.ProductGroups.AsQueryable();
            var volumes = ResourceData.Volumes.AsQueryable();

            var setupDbPi = SetupDb(_mockSetProductInfo, dataProductInfos);
            var setupDbCon = SetupDb(_mockSetContainer, dataContainers);
            var setupDbPg = SetupDb(_mockSetProductGroup, productGroups);
            var setupDbVol = SetupDb(_mockSetVolume, volumes);

            _mockContext.Setup(x => x.ProductsInfoes).Returns(setupDbPi.Object);
            _mockContext.Setup(x => x.Containers).Returns(setupDbCon.Object);
            _mockContext.Setup(x => x.ProductGroups).Returns(setupDbPg.Object);
            _mockContext.Setup(x => x.Volumes).Returns(setupDbVol.Object);

            // Injects mock database.
            var dbProductInfoRepository = new DbProductInfoRepository(_mockContext.Object);
            var dbContainerRepository = new DbContainerRepository(_mockContext.Object);
            var dbProductGroupRepository = new DbProductGroupRepository(_mockContext.Object);
            var dbVolumeRepository = new DbVolumeRepository(_mockContext.Object);


            _productController = new ProductController(dbProductInfoRepository, dbContainerRepository,
                dbProductGroupRepository, dbVolumeRepository);
        }

        public Mock<DbSet<T>> SetupDb<T>(Mock<DbSet<T>> mock, IQueryable<T> data) where T : class
        {
            mock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator);
            return mock;
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
            var productInfo = _mockSetProductInfo.Object.First(x => x.Id == 1);
            _productController.Delete(productInfo, productInfo.Id);
            var result = _mockSetProductInfo.Object.First(x => x.Id == productInfo.Id);
            Assert.AreEqual(true, result.IsRemoved);
        }

        [Test]
        public void Edit_Get_Object()
        {
            var actionResult = _productController.Edit(1);
            var viewResult = actionResult as ViewResult;
            var result = (ProductInfo) viewResult.Model;
            Assert.AreEqual(1, result.Id);
        }

        [Test]
        public void Edit_Update_Db_New_Info_In_Object()
        {
            var productInfos = _mockSetProductInfo.Object.ToList();
            var tempObj = productInfos[0];
            tempObj.Abv = 100;
            _productController.Edit(tempObj);
            Assert.AreEqual(100, productInfos[0].Abv);
        }

        [Test]
        public void Create()
        {
            var productInfo = new ProductInfo
            {
                Id = 3,
                Name = "Cray wine",
                Container = ResourceData.Containers[0],
                Volume = new Volume
                {
                    Id = 3,
                    Milliliter = 750
                },
                Abv = 14,
                PurchasePrice = 30,
                TradingMargin = 5
            };
            _productController.Create(productInfo);
            _mockSetProductInfo.Verify(x => x.Add(productInfo), Times.Once);
            _mockContext.Verify(x => x.SaveChanges(), Times.Once);
        }


        [Test]
        public void Details_Get_Object()
        {
            var actionResult = _productController.Details(1);
            var viewResult = actionResult as ViewResult;
            var result = (ProductInfo) viewResult.Model;
            Assert.AreEqual(1, result.Id);
        }
    }
}