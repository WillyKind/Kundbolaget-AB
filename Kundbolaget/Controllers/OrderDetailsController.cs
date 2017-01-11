using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kundbolaget.EntityFramework.Repositories;
using Kundbolaget.Models.EntityModels;
using Kundbolaget.ViewModels;

namespace Kundbolaget.Controllers
{
    public class OrderDetailsController : Controller
    {
        private DbOrderDetailsRpository _orderDetails;
        private DbOrderRepository _orders;
        private DbProductInfoRepository _products;

        public OrderDetailsController()
        {
            _orderDetails = new DbOrderDetailsRpository();
            _orders = new DbOrderRepository();
            _products = new DbProductInfoRepository();
        }


        public OrderDetailsController(DbOrderDetailsRpository orderDetailsRpository, DbOrderRepository dbOrderRepository,DbProductInfoRepository dbProductInfoRepository)
        {
            _orderDetails = orderDetailsRpository;
            _orders = dbOrderRepository;
            _products = dbProductInfoRepository;
        }
        // GET: OrderDetails
        public ActionResult Index(int id, int companyId)
        {
            var model = new OrderDetailsViewModel();
            model.OrderDetailses = _orders.GetOrderDetails(id);
            model.OrderId = id;
            model.ParentCompanyId = _orders.GetEntity(id).Company.ParentCompanyId.Value;
            return View(model);
        }

        public ActionResult Create(int id)
        {
            var model = new OrderDetailsViewModel();
            model.ProductInfos = _products.GetEntities();
            model.OrderDetails.OrderId = id;
            model.OrderId = id;
            model.ParentCompanyId = _orders.GetEntity(id).Company.ParentCompanyId.Value;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(OrderDetailsViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var product = _products.GetEntity(model.OrderDetails.ProductInfoId);
            model.OrderDetails.UnitPrice = product.Price;
            model.OrderDetails.TotalPrice = model.OrderDetails.Amount*model.OrderDetails.UnitPrice;
            model.OrderDetails.ReservedAmount = 0;
            var updatedOrder = _orders.GetEntity(model.OrderDetails.OrderId);
            updatedOrder.Price +=(int) model.OrderDetails.TotalPrice;

            _orders.UpdateEntity(updatedOrder);
            _orderDetails.CreateEntity(model.OrderDetails);
            return RedirectToAction("Create","OrderDetails",new {id=updatedOrder.Id});
        }


        public ActionResult Edit(int id, int orderId)
        {
            var model = new OrderDetailsViewModel();
            model.OrderDetails = _orderDetails.GetEntity(id);
            model.OrderId = orderId;
            model.ParentCompanyId = _orders.GetEntity(orderId).Company.ParentCompanyId.Value;

            return View(model);
        }

        [HttpPost]
        public bool SaveOrderDetail(int id, int newAmount) {

            var orderDetail = _orderDetails.GetEntity(id);
            orderDetail.Amount = newAmount;
            orderDetail.TotalPrice = orderDetail.UnitPrice*newAmount;
            _orderDetails.UpdateEntity(orderDetail);

            var updatedOrder = _orders.GetEntity(orderDetail.OrderId);
            updatedOrder.Price = updatedOrder.OrderDetails.Sum(updateOrderOrderDetail => (int)updateOrderOrderDetail.TotalPrice);

            _orders.UpdateEntity(updatedOrder);

            if (updatedOrder.OrderPicked == null && updatedOrder.OrderTransported == null)
            {
                UpdateSaldoEditedOrder(id);
            }
            return true;
        }

        [HttpPost]
        public ActionResult Edit(int id, OrderDetailsViewModel model)
        {
            if (model == null)
            {
                return HttpNotFound();
            }
            model.OrderDetails.TotalPrice = model.OrderDetails.Amount*model.OrderDetails.UnitPrice;
            _orderDetails.UpdateEntity(model.OrderDetails);

            model.OrderDetails.Order.Price = model.OrderDetails.Order.OrderDetails.Sum(od => od.TotalPrice);

            _orders.UpdateEntity(model.OrderDetails.Order);
            return RedirectToAction("Index", new {id=model.OrderDetails.Order.Id, companyId= model.OrderDetails.Order.Company.ParentCompanyId});
        }

        public ActionResult Delete(int id, int orderId)
        {
            var model = new OrderDetailsViewModel();
            model.OrderDetails = _orderDetails.GetEntity(id);
            model.OrderId = orderId;
            model.ParentCompanyId = _orders.GetEntity(orderId).Company.ParentCompanyId.Value;

            if (model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(OrderDetailsViewModel model, int id)
        {
            if (model.OrderDetails.Id != id)
            {
                ModelState.AddModelError("Name", "Bad Request");
                return View(model);
            }
            var updatedOrder = _orders.GetEntity(model.OrderId);
            updatedOrder.Price = updatedOrder.Price - (int)model.OrderDetails.TotalPrice;

            var orderDetails = _orderDetails.GetEntity(id);
            foreach (var stock in orderDetails.ProductInfo.ProductStocks)
            {
                stock.Amount += orderDetails.ReservedAmount.Value;
            }
            _orders.UpdateEntity(updatedOrder);
            _orderDetails.DeleteEntity(id);
            return RedirectToAction("Index", new {id=updatedOrder.Id, companyId=updatedOrder.Company.ParentCompanyId});
        }

        [HttpPost]
        public void UpdateSaldoEditedOrder(int id)
        {
            _orders.ReturnProductToStock(id);
        }

        
    }
}