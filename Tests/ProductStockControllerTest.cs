using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kundbolaget.Controllers;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.EntityFramework.Repositories;
using Kundbolaget.Models.EntityModels;
using Kundbolaget.ViewModels;
using Moq;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    class ProductStockControllerTest
    {
        //FAKE CONTEXT
        private Mock<StoreContext> _mockContext;
        //FAKE DbSet
        private Mock<DbSet<ProductStock>> _mockSetProductStock;
        private Mock<DbSet<ProductInfo>> _mockSetProductInfo;
        private Mock<DbSet<Warehouse>> _mockSetWarehouse;
        private ProductStockController _productStockController;

        [SetUp]
        public void Initializer()
        {
            //New that shit up everytime a test runs
            _mockContext = new Mock<StoreContext>();
            _mockSetProductStock = new Mock<DbSet<ProductStock>>();
            _mockSetProductInfo = new Mock<DbSet<ProductInfo>>();
            _mockSetWarehouse = new Mock<DbSet<Warehouse>>();
            //WE NEED DATA
            var productStockList = ResourceData.ProductStockList.AsQueryable();
            var productInfoList = ResourceData.ProductInfoList.AsQueryable();
            var warehouse = ResourceData.WareHouseList.AsQueryable();
            //Setup behavior
            var setupDbPiS = Helper.SetupDb(_mockSetProductStock, productStockList);
            var setupDbPi = Helper.SetupDb(_mockSetProductInfo, productInfoList);
            var setupDbWarehouse = Helper.SetupDb(_mockSetWarehouse, warehouse);
            //Setup behavior
            _mockContext.Setup(x => x.ProductStocks).Returns(setupDbPiS.Object);
            _mockContext.Setup(x => x.ProductsInfoes).Returns(setupDbPi.Object);
            _mockContext.Setup(x => x.Warehouses).Returns(setupDbWarehouse.Object);
            //This will make the mock version of the db approve any string given to the include method.
            //Without this you will get null reference exception when calling include.
            _mockSetProductStock.Setup(x => x.Include(It.IsAny<string>())).Returns(_mockSetProductStock.Object);
            _mockSetProductInfo.Setup(x => x.Include(It.IsAny<string>())).Returns(_mockSetProductInfo.Object);
            _mockSetWarehouse.Setup(x => x.Include(It.IsAny<string>())).Returns(_mockSetWarehouse.Object);

            // Injects mock database via overloaded ctor
            var dbProductInfoRepository = new DbProductStockRepository(_mockContext.Object);
            var productInfoRepository = new DbProductInfoRepository(_mockContext.Object);
            var dbWarehouseRepository = new DbWarehouseRepository(_mockContext.Object);
            //Setup fakerepo via overloaded ctor
            _productStockController = new ProductStockController(dbProductInfoRepository, productInfoRepository , dbWarehouseRepository);
        }
        [Test]
        public void Index_Retrieve_Data()
        {
            var actionResult = _productStockController.Index();
            var viewResult = actionResult as ViewResult;
            var viewResultModel = (ProductStock[])viewResult.Model;
            Assert.AreEqual(1,viewResultModel[0].Id);
        }

        [Test]
        public void Edit_Get_Object()
        {
            var actionResult = _productStockController.Edit(1);
            var viewResult = actionResult as ViewResult;
            var result = (ProductStockVM)viewResult.Model;
            Assert.AreEqual(1, result.ProductStock.Id);
        }

        [Test]
        public void Edit_Update_Db_New_Info_In_Object()
        {
            var productStockVm = new ProductStockVM();
            productStockVm.ProductStock = _mockSetProductStock.Object.First();
            productStockVm.ProductStock.Amount = 2000;
            _productStockController.Edit(productStockVm);
            Assert.AreEqual(2000, productStockVm.ProductStock.Amount);
        }

        [Test]
        public void Create()
        {
            var productStock = ResourceData.ProductStockList.First();
            _productStockController.Create(productStock);
            _mockSetProductStock.Verify(x => x.Add(productStock), Times.Once);
            _mockContext.Verify(x => x.SaveChanges(), Times.Once);
        }


        [Test]
        public void Details_Get_Object()
        {
            var actionResult = _productStockController.Details(1);
            var viewResult = actionResult as ViewResult;
            var result = (ProductStock)viewResult.Model;
            Assert.AreEqual(1, result.Id);
        }

    }
}
