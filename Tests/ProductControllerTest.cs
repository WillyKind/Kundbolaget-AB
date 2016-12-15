﻿using System.Data.Entity;
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
    internal class ProductControllerTest
    {
        [SetUp]
        public void Initializer()
        {
            //New that shit up everytime a test runs
            _mockContext = new Mock<StoreContext>();
            _mockSetProductInfo = new Mock<DbSet<ProductInfo>>();
            _mockSetContainer = new Mock<DbSet<Container>>();
            _mockSetProductGroup = new Mock<DbSet<ProductGroup>>();
            _mockSetVolume = new Mock<DbSet<Volume>>();

            //WE NEED DATA
            var dataProductInfos = ResourceData.ProductInfoList.AsQueryable();
            var dataContainers = ResourceData.Containers.AsQueryable();
            var productGroups = ResourceData.ProductGroups.AsQueryable();
            var volumes = ResourceData.Volumes.AsQueryable();

            //Setup behavior
            var setupDbPi = SetupDb(_mockSetProductInfo, dataProductInfos);
            var setupDbCon = SetupDb(_mockSetContainer, dataContainers);
            var setupDbPg = SetupDb(_mockSetProductGroup, productGroups);
            var setupDbVol = SetupDb(_mockSetVolume, volumes);

            //Setup behavior
            _mockContext.Setup(x => x.ProductsInfoes).Returns(setupDbPi.Object);
            _mockContext.Setup(x => x.Containers).Returns(setupDbCon.Object);
            _mockContext.Setup(x => x.ProductGroups).Returns(setupDbPg.Object);
            _mockContext.Setup(x => x.Volumes).Returns(setupDbVol.Object);

            //This will make the mock version of the db approve any string given to the include method.
            //Without this you will get null reference exception when calling include.
            _mockSetProductInfo.Setup(x => x.Include(It.IsAny<string>())).Returns(_mockSetProductInfo.Object);

            // Injects mock database via overloaded ctor
            var dbProductInfoRepository = new DbProductInfoRepository(_mockContext.Object);
            var dbContainerRepository = new DbContainerRepository(_mockContext.Object);
            var dbProductGroupRepository = new DbProductGroupRepository(_mockContext.Object);
            var dbVolumeRepository = new DbVolumeRepository(_mockContext.Object);


            //Setup fakerepo via overloaded ctor
            _productController = new ProductController(dbProductInfoRepository, dbContainerRepository,
                dbProductGroupRepository, dbVolumeRepository);
        }

        //FAKE CONTEXT
        private Mock<StoreContext> _mockContext;

        //FAKE DbSet
        private Mock<DbSet<ProductInfo>> _mockSetProductInfo;
        private Mock<DbSet<Container>> _mockSetContainer;
        private Mock<DbSet<ProductGroup>> _mockSetProductGroup;
        private Mock<DbSet<Volume>> _mockSetVolume;


        private ProductController _productController;

        public Mock<DbSet<T>> SetupDb<T>(Mock<DbSet<T>> mock, IQueryable<T> data) where T : class
        {
            mock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator);
            return mock;
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
        public void Create_Post_Redirect_To_Index()
        {
            var result = _productController.Create(new ProductInfo
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
                }
            ) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void Delete_Change_IsRemoved()
        {
            var productInfo = _mockSetProductInfo.Object.First(x => x.Id == 1);
            _productController.Delete(productInfo, productInfo.Id);
            var result = _mockSetProductInfo.Object.First(x => x.Id == productInfo.Id);
            Assert.AreEqual(true, result.IsRemoved);
            _mockContext.Verify(x => x.SaveChanges(), Times.Once);
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
        public void Delete_Is_Soft_Delte()
        {
            var productInfo = _mockSetProductInfo.Object.First(x => x.Id == 1);
            _productController.Delete(productInfo, productInfo.Id);
            //Verifies that Remove method is never called.
            _mockSetProductInfo.Verify(x => x.Remove(productInfo), Times.Never);
        }

        [Test]
        public void Delete_Post_Redirect_To_Index()
        {
            var result = _productController.Delete(ResourceData.ProductInfoList[0], 1) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }


        [Test]
        public void Details_Get_Object()
        {
            var actionResult = _productController.Details(1);
            var viewResult = actionResult as ViewResult;
            var result = (ProductInfo) viewResult.Model;

            Assert.AreEqual(ResourceData.ProductInfoList[0].Description, result.Description);
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual(ResourceData.ProductInfoList[0].Name, result.Name);
        }

        [Test]
        public void Edit_Get_Object()
        {
            var actionResult = _productController.Edit(1);
            var viewResult = actionResult as ViewResult;
            var result = (ProductInfo) viewResult.Model;
            Assert.AreEqual(1, result.Id);
            Assert.AreEqual(ResourceData.ProductInfoList[0].Name, result.Name);
            Assert.AreEqual(ResourceData.ProductInfoList[0].Description, result.Description);
            Assert.AreEqual(ResourceData.ProductInfoList[0].Abv, result.Abv);
        }

        [Test]
        public void Edit_Post_Redirect_To_Index()
        {
            var result = _productController.Edit(ResourceData.ProductInfoList[0]) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [Test]
        public void Edit_Update_Db_New_Info_In_Object()
        {
            var productInfos = _mockSetProductInfo.Object.ToList();
            var tempObj = productInfos[0];
            tempObj.Abv = 100;
            _productController.Edit(tempObj);

            Assert.AreEqual(100, productInfos[0].Abv);
            _mockSetProductInfo.Verify(x => x.Attach(tempObj), Times.Once);
            _mockContext.Verify(x => x.SaveChanges(), Times.Once);
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
        public void View_Create()
        {
            var result = _productController.Create() as ViewResult;
            Assert.AreEqual("Create", result.ViewName);
        }

        [Test]
        public void View_Delete_With_Existing_Entity()
        {
            var result = _productController.Delete(1) as ViewResult;
            Assert.AreEqual("Delete", result.ViewName);
        }

        [Test]
        public void View_Delete_With_Existing_Entity_Does_Not_Return_404_Error()
        {
            var result = _productController.Delete(1);
            Assert.AreNotEqual(typeof(HttpNotFoundResult), result.GetType());
        }

        [Test]
        public void View_Delete_Without_Existing_Entity_Return_404_Error()
        {
            var result = _productController.Delete(2000);
            Assert.AreEqual(typeof(HttpNotFoundResult), result.GetType());
        }

        [Test]
        public void View_Detail_With_Existing_Does_Not_Return_404_Error()
        {
            var result = _productController.Details(1);
            Assert.AreNotEqual(typeof(HttpNotFoundResult), result.GetType());
        }

        [Test]
        public void View_Detail_With_Existing_Entity()
        {
            var result = _productController.Details(1) as ViewResult;
            Assert.AreEqual("Details", result.ViewName);
        }

        [Test]
        public void View_Detail_Without_Existing_Entity_Returns_404_Error()
        {
            var result = _productController.Details(2000);
            Assert.AreEqual(typeof(HttpNotFoundResult), result.GetType());
        }

        [Test]
        public void View_Edit_With_Existing_Entity()
        {
            var result = _productController.Edit(1) as ViewResult;
            Assert.AreEqual("Edit", result.ViewName);
        }

        [Test]
        public void View_Edit_With_Existing_Entity_Does_Not_Return_404_Error()
        {
            var result = _productController.Edit(1);
            Assert.AreNotEqual(typeof(HttpNotFoundResult), result.GetType());
        }

        [Test]
        public void View_Edit_Without_Existing_Entity_Return_404_Error()
        {
            var result = _productController.Edit(2000);
            Assert.AreEqual(typeof(HttpNotFoundResult), result.GetType());
        }

        [Test]
        public void View_Index()
        {
            var result = _productController.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}