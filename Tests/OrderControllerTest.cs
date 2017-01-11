using System;
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

namespace Tests
{
    [TestFixture]
    class OrderControllerTest
    {
        //Fake context
        private Mock<StoreContext> _mockContext;
        //Fake dbset<order>
        private Mock<DbSet<Order>> _mockOrder;
        private Mock<DbSet<Company>> _mockCompany;
        private Mock<DbSet<Invoice>> _mockInvoice;
        private Mock<DbSet<ProductStock>> _mockProductStock;

        private OrderController _orderController;

        [SetUp]
        public void Initializer()
        {
            //New stuff upp
            _mockContext = new Mock<StoreContext>();
            _mockOrder = new Mock<DbSet<Order>>();
            _mockCompany = new Mock<DbSet<Company>>();
            _mockInvoice = new Mock<DbSet<Invoice>>();
            _mockProductStock = new Mock<DbSet<ProductStock>>();

            //Fetch data
            var dataOrders = OrderResourcesData.DummyOrder.AsQueryable();
            var dataCompanies = ResourceData.Companies.AsQueryable();
            var dataInvoices = ResourceData.Invoices.AsQueryable();
            var dataProductStocks = ResourceData.ProductStockList.AsQueryable();

            //setup behaviour
            var setupDbOrder = Helper.SetupDb(_mockOrder, dataOrders);
            var setupCompanies = Helper.SetupDb(_mockCompany, dataCompanies);
            var setupInvoice = Helper.SetupDb(_mockInvoice, dataInvoices);
            var setupProductStocks = Helper.SetupDb(_mockProductStock, dataProductStocks);

            //setup behaviour
            _mockContext.Setup(x => x.Orders).Returns(setupDbOrder.Object);
            _mockContext.Setup(x => x.Companies).Returns(setupCompanies.Object);
            _mockContext.Setup(x => x.Invoices).Returns(setupInvoice.Object);
            _mockContext.Setup(x => x.ProductStocks).Returns(setupProductStocks.Object);

            //inject mock database via overloaded ctor
            var dbOrdersRepository = new DbOrderRepository(_mockContext.Object);
            var dbCompanyRepository = new DbCompanyRepository(_mockContext.Object);
            var dbInvoiceRepository = new DbInvoiceRepository(_mockContext.Object);
            var dbProductStockRepository = new DbProductStockRepository(_mockContext.Object);

            //setup fake controller
            _orderController = new OrderController(dbOrdersRepository, dbCompanyRepository, dbInvoiceRepository, dbProductStockRepository);
        }

        [Test]
        public void Create()
        {
            var orderViewModel = new OrderViewModel();

            var order = new Order
            {
                Company = new Company
                {
                    Address = new Address {Street = "Besöksvägen", Number = "1A", ZipCode = "111 11"}
                    ,
                    ContactPerson = new ContactPerson
                    {
                        FirstName = "Viktor",
                        LastName = "Gustafsson",
                        Email = "Viktor@randomcompany.com",
                        PhoneNumber = "+46899 99 99"
                    },
                    Country = new Country {Name = "Sweden", CountryCode = "+46", Region = "EMEA"},
                    DeliveryAddress = new Address {Street = "Besöksvägen", Number = "1A", ZipCode = "111 11"},
                    Email = "icavarberg@ica.com",
                    PhoneNumber = "+46899 11 11",
                    ParentCompany = new Company
                    {
                        Address = new Address {Street = "Besöksvägen", Number = "1A", ZipCode = "111 11"},
                        ContactPerson = new ContactPerson
                        {
                            FirstName = "Viktor",
                            LastName = "Gustafsson",
                            Email = "Viktor@randomcompany.com",
                            PhoneNumber = "+46899 99 99"
                        },
                        Country = new Country {Name = "Sweden", CountryCode = "+46", Region = "EMEA"},
                        DeliveryAddress = new Address {Street = "Besöksvägen", Number = "1A", ZipCode = "111 11"},
                        Email = "icagruppen@ica.com",
                        Name = "IcaGruppen",
                        PhoneNumber = "+56899 22 22"
                    },
                    Name = "Ica Vårberg"
                },
                CreatedDate = DateTime.Now,
                WishedDeliveryDate = DateTime.Parse("2016-12-12"),
            };
            orderViewModel.Order = order;
            _orderController.Create(orderViewModel,order.Company.ParentCompany.Id);
            _mockOrder.Verify(x => x.Add(order), Times.Once);
            _mockContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        
        [Test]
        public void Index_Retrieve_All_Data()
        {
            var actionResult = _orderController.Index();
            var viewResult = actionResult as ViewResult;
            var viewResultModel = (Company[])viewResult.Model;
            var companies = viewResultModel.ToList();
            Assert.AreEqual(2, companies.Count);
        }

        [Test]
        public void Company_GetParentCompanyId()
        {
            var actionResult = _orderController.Company(1);
            var viewResult = actionResult as ViewResult;
            var viewResultModel = (OrderViewModel) viewResult.Model;
            Assert.AreEqual(1, viewResultModel.ParentCompanyId);
        }

        [Test]
        public void Delete()
        {
            //Knepigt med bra test
           
        }

        [Test]
        public void RestoreProductStock()
        {
            var order = OrderResourcesData.DummyOrder.First();
            
            _orderController.RestoreProductStock(order);
            
        }
    }
}