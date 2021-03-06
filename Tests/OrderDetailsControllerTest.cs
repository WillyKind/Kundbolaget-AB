﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Kundbolaget.Controllers;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.EntityFramework.Repositories;
using Kundbolaget.Models.EntityModels;
using Kundbolaget.ViewModels;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Tests
{
    [TestFixture]
    internal class OrderDetailsControllerTest
    {
        //Fake context
        private Mock<StoreContext> _mockContext;
        private OrderDetailsController _orderDetailsController;

        //Fake dbset
        private Mock<DbSet<OrderDetails>> _mockSetOrderDetails;
        private Mock<DbSet<ProductInfo>> _mockSetProductInfo;
        private Mock<DbSet<Order>> _mockSetOrder;


        [SetUp]
        public void Initializer()
        {
            //New stuff upp
            _mockContext = new Mock<StoreContext>();
            _mockSetOrderDetails = new Mock<DbSet<OrderDetails>>();
            _mockSetOrder = new Mock<DbSet<Order>>();
            _mockSetProductInfo = new Mock<DbSet<ProductInfo>>();

            //Fetch data
            var dataOrderDetails = OrderDetailsResources.DummyOrderDetails.AsQueryable();
            var dataProductInfos = ResourceData.ProductInfoList.AsQueryable();
            var dataOrders = OrderResourcesData.DummyOrder.AsQueryable();

            //setup behaviour
            var setupDbOrderDetails = Helper.SetupDb(_mockSetOrderDetails, dataOrderDetails);
            var setupDbProductInfos = Helper.SetupDb(_mockSetProductInfo, dataProductInfos);
            var setupDbOrders = Helper.SetupDb(_mockSetOrder, dataOrders);

            //setup behaviour
            _mockContext.Setup(x => x.OrderDetails).Returns(setupDbOrderDetails.Object);
            _mockContext.Setup(x => x.ProductsInfoes).Returns(setupDbProductInfos.Object);
            _mockContext.Setup(x => x.Orders).Returns(setupDbOrders.Object);


            _mockContext.Setup(x => x.SetModified(It.IsAny<OrderDetails>()));
            _mockContext.Setup(x => x.SetAdded(It.IsAny<OrderDetails>()));
            _mockContext.Setup(x => x.SetModified(It.IsAny<Order>()));
            _mockContext.Setup(x => x.SetAdded(It.IsAny<Order>()));

            //inject mock database via overloaded ctor
            var dbOrderDetailsRepository = new DbOrderDetailsRpository(_mockContext.Object);
            var dbOrdersRepository = new DbOrderRepository(_mockContext.Object);
            var dbProductInfoRepository = new DbProductInfoRepository(_mockContext.Object);

            //This will make the mock version of the db approve any string given to the include method.
            //Without this you will get null reference exception when calling include.
            _mockSetOrderDetails.Setup(x => x.Include(It.IsAny<string>())).Returns(_mockSetOrderDetails.Object);
            _mockSetOrder.Setup(x => x.Include(It.IsAny<string>())).Returns(_mockSetOrder.Object);
            _mockSetProductInfo.Setup(x => x.Include(It.IsAny<string>())).Returns(_mockSetProductInfo.Object);


            //setup fake controller
            _orderDetailsController = new OrderDetailsController(dbOrderDetailsRepository, dbOrdersRepository,
                dbProductInfoRepository);
        }

        [Test]
        public void Create_To_Database()
        {
            var model = new OrderDetailsViewModel();
            var orderDetails = new OrderDetails
            {
                Amount = 10,
                IsRemoved = false,
                OrderId = 0,
                ProductInfoId = 1,
                UnitPrice = 50,
                TotalPrice = 5000
            };
            model.OrderDetails = orderDetails;
            _orderDetailsController.Create(model);
            _mockSetOrderDetails.Verify(x => x.Add(orderDetails), Times.Once);
            _mockContext.Verify(x => x.SaveChanges(), Times.Exactly(2));
        }

        [Test]
        public void Edit_Action_Result()
        {
            var model = new OrderDetailsViewModel();
            var orderDetails = new OrderDetails
            {
                Id = 1,
                Amount = 10,
                IsRemoved = false,
                OrderId = 0,
                ProductInfoId = 1,
                UnitPrice = 50,
                TotalPrice = 5000
            };
            model.OrderDetails = orderDetails;
            model.OrderDetails.Order = OrderResourcesData.DummyOrder[0];
            var actionResult = (RedirectToRouteResult) _orderDetailsController.Edit(model.OrderDetails.Id, model);
            var result = actionResult.RouteValues;
            Assert.IsTrue(result.ContainsKey("id"));
            Assert.IsTrue(result.ContainsKey("companyId"));
            Assert.IsTrue(result.ContainsKey("action"));
            Assert.IsTrue(result.ContainsValue(0));
            Assert.IsTrue(result.ContainsValue("Index"));
        }

        [Test]
        public void Edit_Update_Database()
        {
            var model = new OrderDetailsViewModel();
            var orderDetails = new OrderDetails
            {
                Id = 1,
                Amount = 10,
                IsRemoved = false,
                OrderId = 0,
                Order = OrderResourcesData.DummyOrder[0],
                ProductInfoId = 1,
                UnitPrice = 50,
                TotalPrice = 5000
            };
            model.OrderDetails = orderDetails;
            _orderDetailsController.Edit(model.OrderDetails.Id, model);
            _mockSetOrderDetails.Verify(x=>x.Attach(orderDetails));
            _mockContext.Verify(x => x.SaveChanges(), Times.Exactly(2));
        }

        [Test]
        public void Index_Get_OrderDetails_For_Order()
        {
            var actionResult = _orderDetailsController.Index(0);
            var viewResult = actionResult as ViewResult;
            var orderDetailsViewModel = (OrderDetailsViewModel) viewResult.Model;
            var orderDetailses = orderDetailsViewModel.OrderDetailses;
            Assert.AreEqual(10,orderDetailses.First().Amount);
        }

        [Test]
        public void SaveOrderDetail()
        {
            var orderDetails = _mockSetOrderDetails.Object.First().Amount;
            _orderDetailsController.SaveOrderDetail(1, 1000);
            var orderDetailsSaveNewAmount = _mockSetOrderDetails.Object.First();
            Assert.AreNotEqual(orderDetails, orderDetailsSaveNewAmount.Amount);
        }


        //Test no longer works since since deletion of order details now is handeled by JS and ajax
        //[Test]
        //public void Delete_Get_Correct_OrderDetail_For_Deletion()
        //{
        //    var actionResult = _orderDetailsController.Delete(OrderDetailsResources.DummyOrderDetails.First().Id, 0);
        //    var viewResult = actionResult as ViewResult;
        //    var orderDetailsViewModel = (OrderDetailsViewModel) viewResult.Model;
        //    Assert.AreEqual(OrderDetailsResources.DummyOrderDetails.First().Id, orderDetailsViewModel.OrderDetails.Id);
        //}
    }
}