using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Kundbolaget.Controllers;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.EntityFramework.Repositories;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Kundbolaget.Models.EntityModels;


namespace Tests
{
    [TestFixture]
    class AdressControllerTests
    {
        
         Mock<StoreContext> _mockContext;
         Mock<DbSet<Address>> _mockAdress;
         AddressController _adressController;

        [SetUp]
        public void Initializer()
        {
            _mockContext = new Mock<StoreContext>();
            _adressController = new AddressController();
            _mockAdress = new Mock<DbSet<Address>>();
            var dataAdressList = ResourceData.AdressList.AsQueryable();
            var setupDbAddress = Helper.SetupDb(_mockAdress, dataAdressList);
            _mockContext.Setup(x => x.Addresses).Returns(setupDbAddress.Object);
            _mockAdress.Setup(x => x.Include(It.IsAny<string>())).Returns(_mockAdress.Object);
            var dbAddressRepository = new DbAddressRepository(_mockContext.Object);
            _adressController = new AddressController(dbAddressRepository);
        }

        [Test]
        public void Index_Retrieve_All_Data()
        {
            var actionResult = _adressController.Index();
            var viewResult = actionResult as ViewResult;
            var viewResultModel = (Address[]) viewResult.Model;
            Assert.AreEqual(2, viewResultModel.Length);
        }

        [Test]
        public void Index_Correct_View()
        {
            var viewResult = (ViewResult) _adressController.Index();
            Assert.AreSame("Index", viewResult.ViewName);
        }


        [Test]
        public void Edit_Get_Specific_Adress()
        {
            var actionResult = _adressController.Edit(1);
            var viewResult = actionResult as ViewResult;
            var viewResultModel = (Address) viewResult.Model;
            Assert.AreEqual(1, viewResultModel.Id);
        }
        [Test]
        public void Edit_Change_Value_In_Adress()
        {
            var address = _mockAdress.Object.ToList();
            address[0].ZipCode = "112";
            _adressController.Edit(address[0]);
            Assert.AreEqual("112", _mockAdress.Object.First().ZipCode);
        }

        [Test]
        public void Edit_Correct_View()
        {
            var viewResult = (ViewResult) _adressController.Edit(1);
            Assert.AreSame("Edit", viewResult.ViewName);
        }

        [Test]
        public void Edit_Redirect_To_Action()
        {
            var result = _adressController.Edit(ResourceData.AdressList[0]) as RedirectToRouteResult;
            Assert.AreSame("Index", result.RouteValues["action"]);
        }


        [Test]
        public void Delete_Get_Adress()
        {
            var actionResult = _adressController.Delete(1);
            var viewResult = actionResult as ViewResult;
            var viewResultModel = (Address) viewResult.Model;
            Assert.AreEqual(1, viewResultModel.Id);
        }

        [Test]
        public void Delete_Change_Bool_IsRemoved()
        {
            var addresses = _mockAdress.Object.ToList();
            _adressController.Delete(addresses[0], addresses[0].Id);
            Assert.AreEqual(true , _mockAdress.Object.First().IsRemoved);
        }

        [Test]
        public void Delete_Correct_View()
        {
            var viewResult = (ViewResult) _adressController.Delete(1);
            Assert.AreSame("Delete", viewResult.ViewName);
        }

        [Test]
        public void Delete_Redirect_To_Action()
        {
            var result = _adressController.Delete(ResourceData.AdressList[0], ResourceData.AdressList[0].Id) as RedirectToRouteResult;
            Assert.AreSame("Index", result.RouteValues["action"]);
        }

        [Test]
        public void Create_Correct_Empty_View()
        {
            var viewResult = (ViewResult) _adressController.Create();
            Assert.AreEqual("Create", viewResult.ViewName);
        }

        [Test]
        public void Create_Redirect_To_Action()
        {
            var result = _adressController.Create(ResourceData.AdressList[0]) as RedirectToRouteResult;
            Assert.AreSame("Index", result.RouteValues["action"]);
        }

        [Test]
        public void Details_Correct_View()
        {
            var viewResult = (ViewResult) _adressController.Details(1);
            Assert.AreEqual("Details", viewResult.ViewName);
        }


        [Test]
        public void Create()
        {
            var address = new Address
            {
                Id = 1,
                ZipCode = "18238",
                Number = "1",
                Street = "Blomstigen",

            };
            _adressController.Create(address);
            _mockAdress.Verify(x => x.Add(address), Times.Once);
            _mockContext.Verify(x => x.SaveChanges(), Times.Once);
        }
    }
}
