using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Kundbolaget.Controllers;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.EntityFramework.Repositories;
using Kundbolaget.Models.EntityModels;
using Kundbolaget.ViewModels;
using Moq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    internal class FileControllerTest
    {
        [SetUp]
        public void Initalize()
        {
            _mockedContext = new Mock<StoreContext>();
            _mockedDbCompanyRepository = new Mock<DbCompanyRepository>(_mockedContext.Object);
            _mockedDbOrderRepository = new Mock<DbOrderRepository>(_mockedContext.Object);
            _mockedDbProductInfoRepository = new Mock<DbProductInfoRepository>();

            _mockedProductInfoDbSet = new Mock<DbSet<ProductInfo>>();
            _mockedCompanyDbSet = new Mock<DbSet<Company>>();
            _mockedOrderDbSet = new Mock<DbSet<Order>>();

            //Gets all data from the database and puts it in memory for mocked database.
            var setupDbComp = Helper.SetupDb(_mockedCompanyDbSet, Context.Companies.ToArray().AsQueryable());
            var setupDbOrder = Helper.SetupDb(_mockedOrderDbSet, Context.Orders.ToArray().AsQueryable());
            var setupDbPi = Helper.SetupDb(_mockedProductInfoDbSet, Context.ProductsInfoes.ToArray().AsQueryable());

            _mockedContext.Setup(x => x.Companies).Returns(setupDbComp.Object);
            _mockedContext.Setup(x => x.Orders).Returns(setupDbOrder.Object);
            _mockedContext.Setup(x => x.ProductsInfoes).Returns(setupDbPi.Object);
            _mockedContext.Setup(x => x.SetModified(It.IsAny<ProductInfo>()));

            _mockedOrderDbSet = new Mock<DbSet<Order>>();
            _mockedProductInfoDbSet = new Mock<DbSet<ProductInfo>>();

            _fileController = new FileController(
                _mockedDbOrderRepository.Object,
                _mockedDbCompanyRepository.Object,
                _mockedDbProductInfoRepository.Object);
        }

        private Mock<StoreContext> _mockedContext;
        private FileController _fileController;
        private Mock<DbOrderRepository> _mockedDbOrderRepository;
        private Mock<DbCompanyRepository> _mockedDbCompanyRepository;
        private Mock<DbProductInfoRepository> _mockedDbProductInfoRepository;

        private Mock<DbSet<Company>> _mockedCompanyDbSet;
        private Mock<DbSet<Order>> _mockedOrderDbSet;
        private Mock<DbSet<ProductInfo>> _mockedProductInfoDbSet;
        private static readonly StoreContext Context = new StoreContext();

        [Test]
        public void SuccessFull_Order_By_Coop()
        {
            var mock = new Mock<HttpPostedFileBase>();
            mock.Setup(x => x.FileName).Returns("TestiTest.json");
            var success = Orders.OrderCoop;
            mock.Setup(x => x.InputStream)
                .Returns(() => new MemoryStream(Encoding.UTF8.GetBytes(Json.Encode(success) ?? "")));

            var viewResult =
                _fileController.UploadJson(new FileUploadViewModel {OrderFile = success, File = mock.Object})
                    as ViewResult;

            Assert.AreEqual("OrderFileSuccess", viewResult.ViewName, viewResult.ViewName);
        }

        [Test]
        public void SuccessFull_Order_By_Ica()
        {
            var mock = new Mock<HttpPostedFileBase>();
            mock.Setup(x => x.FileName).Returns("TestiTest.json");
            var success = Orders.OrderIca;
            mock.Setup(x => x.InputStream)
                .Returns(() => new MemoryStream(Encoding.UTF8.GetBytes(Json.Encode(success) ?? "")));

            var viewResult =
                _fileController.UploadJson(new FileUploadViewModel {OrderFile = success, File = mock.Object})
                    as ViewResult;

            Assert.AreEqual("OrderFileSuccess", viewResult.ViewName, viewResult.ViewName);
        }

        [Test]
        public void UnsuccessFull_Order_By_Coop_Reason_New_Company()
        {
            var mock = new Mock<HttpPostedFileBase>();
            mock.Setup(x => x.FileName).Returns("TestiTest.json");
            var newCoop = Orders.OrderNewCoop;
            mock.Setup(x => x.InputStream)
                .Returns(
                    () => new MemoryStream(Encoding.UTF8.GetBytes(Json.Encode(newCoop) ?? "")));

            var viewResult =
                _fileController.UploadJson(new FileUploadViewModel
                    {
                        OrderFile = newCoop,
                        File = mock.Object
                    })
                    as ViewResult;

            Assert.AreEqual("OrderFileError", viewResult.ViewName, viewResult.ViewName);
        }

        [Test]
        public void UnsuccessFull_Order_By_Ica_Reason_A_None_Existing_Product()
        {
            var mock = new Mock<HttpPostedFileBase>();
            mock.Setup(x => x.FileName).Returns("TestiTest.json");
            var noneExistingProduct = Orders.OrderIcaWithANoneExistingProduct;
            mock.Setup(x => x.InputStream)
                .Returns(
                    () => new MemoryStream(Encoding.UTF8.GetBytes(Json.Encode(noneExistingProduct) ?? "")));

            var viewResult =
                _fileController.UploadJson(new FileUploadViewModel
                    {
                        OrderFile = noneExistingProduct,
                        File = mock.Object
                    })
                    as ViewResult;

            Assert.AreEqual("OrderFileError", viewResult.ViewName, viewResult.ViewName);
        }

        [Test]
        public void UnsuccessFull_Order_By_Ica_Reason_Negative_Number()
        {
            var mock = new Mock<HttpPostedFileBase>();
            mock.Setup(x => x.FileName).Returns("TestiTest.json");
            var negativeNumber = Orders.IcaNegativeOrderAmmount;
            mock.Setup(x => x.InputStream)
                .Returns(() => new MemoryStream(Encoding.UTF8.GetBytes(Json.Encode(negativeNumber) ?? "")));

            var viewResult =
                _fileController.UploadJson(new FileUploadViewModel
                    {
                        OrderFile = negativeNumber,
                        File = mock.Object
                    })
                    as ViewResult;

            Assert.AreEqual("OrderFileError", viewResult.ViewName, viewResult.ViewName);
        }

        [Test]
        public void UnsuccessFull_Order_By_Ica_Reason_Wrong_Company_Id()
        {
            var mock = new Mock<HttpPostedFileBase>();
            mock.Setup(x => x.FileName).Returns("TestiTest.json");
            var wrongCompanyId = Orders.OrderIcaWrongCompanyId;
            mock.Setup(x => x.InputStream)
                .Returns(
                    () => new MemoryStream(Encoding.UTF8.GetBytes(Json.Encode(wrongCompanyId) ?? "")));

            var viewResult =
                _fileController.UploadJson(new FileUploadViewModel
                    {
                        OrderFile = wrongCompanyId,
                        File = mock.Object
                    })
                    as ViewResult;

            Assert.AreEqual("OrderFileError", viewResult.ViewName, viewResult.ViewName);
        }

        [Test]
        public void UnsuccessFull_Order_By_Ica_Reason_Wrong_Date()
        {
            var mock = new Mock<HttpPostedFileBase>();
            mock.Setup(x => x.FileName).Returns("TestiTest.json");
            var wrongDate = Orders.OrderIcaWrongDate;
            mock.Setup(x => x.InputStream)
                .Returns(
                    () => new MemoryStream(Encoding.UTF8.GetBytes(Json.Encode(wrongDate) ?? "")));

            var viewResult =
                _fileController.UploadJson(new FileUploadViewModel
                    {
                        OrderFile = wrongDate,
                        File = mock.Object
                    })
                    as ViewResult;

            Assert.AreEqual("OrderFileError", viewResult.ViewName, viewResult.ViewName);
        }

        [Test]
        public void UnsuccessFull_Order_By_Ica_Reason_Wrong_Mother_Company_Id()
        {
            var mock = new Mock<HttpPostedFileBase>();
            mock.Setup(x => x.FileName).Returns("TestiTest.json");
            var wrongMotherCompany = Orders.OrderIcaWrongMotherCompanyId;
            mock.Setup(x => x.InputStream)
                .Returns(
                    () => new MemoryStream(Encoding.UTF8.GetBytes(Json.Encode(wrongMotherCompany) ?? "")));

            var viewResult =
                _fileController.UploadJson(new FileUploadViewModel
                    {
                        OrderFile = wrongMotherCompany,
                        File = mock.Object
                    })
                    as ViewResult;

            Assert.AreEqual("OrderFileError", viewResult.ViewName, viewResult.ViewName);
        }
    }
}