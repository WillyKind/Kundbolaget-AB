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

        private OrderController _orderController;

        [SetUp]
        public void Initializer()
        {
            //New stuff upp
            _mockContext = new Mock<StoreContext>();
            _mockOrder = new Mock<DbSet<Order>>();

            //Fetch data
            var dataOrders = OrderResourcesData.DummyOrder.AsQueryable();

            //setup behaviour
            var setupDbOrder = Helper.SetupDb(_mockOrder, dataOrders);

            //setup behaviour
            _mockContext.Setup(x => x.Orders).Returns(setupDbOrder.Object);

            //inject mock database via overloaded ctor
            var dbOrdersRepository = new DbOrderRepository(_mockContext.Object);

            //setup fake controller
            _orderController = new OrderController(dbOrdersRepository);
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
    }
}