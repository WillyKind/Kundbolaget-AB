using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Kundbolaget.Controllers;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.EntityFramework.Repositories;
using Kundbolaget.Models.EntityModels;
using Kundbolaget.ViewModels;
using Moq;
using NUnit.Framework;
using static Tests.Helper;

namespace Tests
{
    [TestFixture]
    public class CompanyControllerTest
    {
        [SetUp]
        public void Initalizer()
        {
            //New that shit up everytime a test runs
            _mockContext = new Mock<StoreContext>();
            _mockAddresses = new Mock<DbSet<Address>>();
            _mockCompanies = new Mock<DbSet<Company>>();
            _mockContactPersons = new Mock<DbSet<ContactPerson>>();
            _mockCountries = new Mock<DbSet<Country>>();

            var companies = new List<Company>
            {
                new Company
                {
                    Id = 1,
                    AddressId = 1,
                    Address = new Address
                    {
                        Id = 1,
                        Street = "TestGatan1",
                        Number = "1A",
                        ZipCode = "00323"
                    },
                    DeliveryAddressId = 2,
                    DeliveryAddress = new Address
                    {
                        Id = 2,
                        Street = "TestGatan2",
                        Number = "2A",
                        ZipCode = "99323"
                    },
                    ContactPersonId = 1,
                    ContactPerson = new ContactPerson
                    {
                        Id = 1,
                        Email = "bob@testing.com",
                        PhoneNumber = "0884934",
                        FirstName = "Bob",
                        LastName = "Doe"
                    },
                    CountryId = 1,
                    Country = new Country
                    {
                        Id = 1,
                        Name = "SwedenTest",
                        CountryCode = "+46",
                        Region = "EuropeTest"
                    },
                    ParentCompanyId = null,
                    ParentCompany = null,
                    Email = "ica@testing.com",
                    Name = "Ica",
                    PhoneNumber = "08329328"
                },
                new Company
                {
                    Id = 2,
                    AddressId = 3,
                    Address = new Address
                    {
                        Id = 3,
                        Street = "TestingWay1",
                        Number = "1A",
                        ZipCode = "00323"
                    },
                    DeliveryAddressId = 4,
                    DeliveryAddress = new Address
                    {
                        Id = 4,
                        Street = "TestinWay3",
                        Number = "2A",
                        ZipCode = "99323"
                    },
                    ContactPersonId = 2,
                    ContactPerson = new ContactPerson
                    {
                        Id = 2,
                        Email = "lisa@testing.com",
                        PhoneNumber = "03293203",
                        FirstName = "Lisa",
                        LastName = "Jones"
                    },
                    CountryId = 2,
                    Country = new Country
                    {
                        Id = 2,
                        Name = "NorwayTest",
                        CountryCode = "+47",
                        Region = "EuropeTest"
                    },
                    ParentCompanyId = null,
                    ParentCompany = null,
                    Email = "coop@testing.com",
                    Name = "Coop",
                    PhoneNumber = "0903232301"
                }
            }.AsQueryable();
            var setupCompany = SetupDb(_mockCompanies, companies).Object;
            SetupDb(_mockAddresses, setupCompany.Select(x => x.Address));
            SetupDb(_mockContactPersons, setupCompany.Select(x => x.ContactPerson));
            SetupDb(_mockCountries, setupCompany.Select(x => x.Country));


            //This will make the mock version of the db approve any string given to the include method.
            //Without this you will get null reference exception when calling include.
            _mockCompanies.Setup(x => x.Include(It.IsAny<string>())).Returns(() => setupCompany);

            _mockContext.Setup(x => x.Companies).Returns(_mockCompanies.Object);
            _mockContext.Setup(x => x.Addresses).Returns(_mockAddresses.Object);
            _mockContext.Setup(x => x.Countries).Returns(_mockCountries.Object);
            _mockContext.Setup(x => x.ContactPersons).Returns(_mockContactPersons.Object);

            var dbCompanyRepository = new DbCompanyRepository(_mockContext.Object);
            var dbAddressRepository = new DbAddressRepository(_mockContext.Object);
            var dbContactPersonRepository = new DbContactPersonRepository(_mockContext.Object);
            var dbCountryRepository = new DbCountryRepository(_mockContext.Object);

            //Setup fakerepo via overloaded ctor
            _companyController = new CompanyController(dbCompanyRepository, dbAddressRepository, dbCountryRepository,
                dbContactPersonRepository);
        }

        private Mock<StoreContext> _mockContext;

        private CompanyController _companyController;
        private Mock<DbSet<Company>> _mockCompanies;
        private Mock<DbSet<Address>> _mockAddresses;
        private Mock<DbSet<ContactPerson>> _mockContactPersons;
        private Mock<DbSet<Country>> _mockCountries;

        [Ignore("This test is relevant when Choosing a contactperson is no longer a dropdown.")]
        public void Edit_Change_Company_Navigation_Property_ContactPerson()
        {
            var viewResult = (ViewResult) _companyController.Edit(1);
            var companyViewModel = (CompanyViewModel) viewResult.Model;
            companyViewModel.Company.ContactPerson.FirstName = "TestBob";
            _companyController.Edit(companyViewModel);
            var company = _mockCompanies.Object.First(c => c.Id == 1);
            Assert.AreSame("TestBob", company.ContactPerson.FirstName);
            _mockCompanies.Verify(c => c.Attach(companyViewModel.Company), Times.Once);
            _mockContactPersons.Verify(c => c.Attach(companyViewModel.Company.ContactPerson), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Test]
        public void Create_Object()
        {
            var companyViewModel = new CompanyViewModel
            {
                Company = new Company(),
                Addresses = _mockAddresses.Object.ToArray(),
                ParentCompanies = _mockCompanies.Object.ToArray(),
                ContactPersons = _mockContactPersons.Object.ToArray(),
                Countries = _mockCountries.Object.ToArray()
            };
            var viewResult = _companyController.Create(companyViewModel);

            _mockCompanies.Verify(x => x.Add(companyViewModel.Company), Times.Once);
            _mockContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Test]
        public void Create_Post_Redirection()
        {
            var viewResult = (RedirectToRouteResult) _companyController.Create(new CompanyViewModel());
            Assert.AreEqual("Index", viewResult.RouteValues["action"]);
        }

        [Test]
        public void Create_View()
        {
            var viewResult = (ViewResult) _companyController.Create();
            Assert.AreEqual("Create", viewResult.ViewName);
        }

        [Test]
        public void Edit_Change_Company_Navigation_Property_Address_Object()
        {
            var viewResult = (ViewResult) _companyController.Edit(1);
            var companyViewModel = (CompanyViewModel) viewResult.Model;
            companyViewModel.Company.Address.Street = "TestStreet";
            _companyController.Edit(companyViewModel);
            var company = _mockCompanies.Object.First(c => c.Id == 1);
            Assert.AreEqual("TestStreet", company.Address.Street);
            Assert.AreNotEqual("TestStreet", company.DeliveryAddress.Street);
            _mockCompanies.Verify(c => c.Attach(companyViewModel.Company), Times.Once);
            _mockAddresses.Verify(c => c.Attach(companyViewModel.Company.Address), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Test] //TODO
        public void Edit_Change_Company_Navigation_Property_Address_Sharing_Id_With_DeliveryAddress()
        {
        }

        [Test]
        public void Edit_Change_Company_Navigation_Property_DeliveryAddress_Object()
        {
            var viewResult = (ViewResult) _companyController.Edit(1);
            var companyViewModel = (CompanyViewModel) viewResult.Model;
            companyViewModel.Company.DeliveryAddress.Street = "TestStreet";
            _companyController.Edit(companyViewModel);
            var company = _mockCompanies.Object.First(c => c.Id == 1);
            Assert.AreSame("TestStreet", company.DeliveryAddress.Street);
            Assert.AreNotEqual("TestStreet", company.Address.Street);
            _mockCompanies.Verify(c => c.Attach(companyViewModel.Company), Times.Once);
            _mockAddresses.Verify(c => c.Attach(companyViewModel.Company.DeliveryAddress), Times.Once);
            _mockAddresses.Verify(c => c.Attach(companyViewModel.Company.Address), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Test]
        public void Edit_Change_Company_Object()
        {
            var viewResult = (ViewResult) _companyController.Edit(1);
            var companyViewModel = (CompanyViewModel) viewResult.Model;
            companyViewModel.Company.Name = "TestName";
            _companyController.Edit(companyViewModel);
            var company = _mockCompanies.Object.First(c => c.Id == 1);
            Assert.AreEqual("TestName", company.Name);
            _mockCompanies.Verify(c => c.Attach(companyViewModel.Company));
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Test]
        public void Edit_Post_Redirection()
        {
            var viewResult =
                (RedirectToRouteResult)
                _companyController.Edit(new CompanyViewModel
                {
                    Company = new Company {Address = new Address(), DeliveryAddress = new Address()}
                });
            Assert.AreEqual("Index", viewResult.RouteValues["action"]);
        }

        [Test]
        public void Edit_View_With_Existing_Entity()
        {
            var viewResult = (ViewResult) _companyController.Edit(1);
            Assert.AreEqual("Edit", viewResult.ViewName);
        }

        [Test]
        public void Edit_View_With_None_Existing_Entity()
        {
            var viewResult = _companyController.Edit(2000);
            Assert.AreEqual(typeof(HttpNotFoundResult), viewResult.GetType());
        }

        [Test]
        public void Edit_ViewResult_Model_Is_CompanyViewModel()
        {
            var viewResult = (ViewResult) _companyController.Edit(1);
            Assert.IsInstanceOf<CompanyViewModel>(viewResult.Model);
        }
    }
}