using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kundbolaget.Controllers;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.EntityFramework.Repositories;
using Kundbolaget.Models.EntityModels;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Tests
{
    [TestFixture]
    internal class CustomerProductControllerTest
    {
        //FAKE CONTEXT
        private Mock<StoreContext> _mockContext;
        private CustomerProductController _customerProductController;

        //FAKE DbSet
        private Mock<DbSet<ProductInfo>> _mockSetProductInfo;
        private Mock<DbSet<Container>> _mockSetContainer;
        private Mock<DbSet<ProductGroup>> _mockSetProductGroup;
        private Mock<DbSet<Volume>> _mockSetVolume;

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
            var setupDbPi = Helper.SetupDb(_mockSetProductInfo, dataProductInfos);
            var setupDbCon = Helper.SetupDb(_mockSetContainer, dataContainers);
            var setupDbPg = Helper.SetupDb(_mockSetProductGroup, productGroups);
            var setupDbVol = Helper.SetupDb(_mockSetVolume, volumes);

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

            _customerProductController = new CustomerProductController(dbProductInfoRepository);
        }

        [Test]
        public void View_Index()
        {
            var result = _customerProductController.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

        [Test]
        public void Index_Retrieve_All_Data()
        {
            var actionResult = _customerProductController.Index();
            var viewResult = actionResult as ViewResult;
            var viewResultModel = (ProductInfo[])viewResult.Model;
            var productInfos = viewResultModel.ToList();
            Assert.AreEqual(2, productInfos.Count);
            Assert.IsTrue(productInfos.All(x => x.ProductGroup.Category.Name == "Öl"));
        }
    }
}
