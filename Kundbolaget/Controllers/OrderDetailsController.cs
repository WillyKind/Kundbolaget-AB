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

        public OrderDetailsController()
        {
            _orderDetails = new DbOrderDetailsRpository();
            _orders = new DbOrderRepository();
        }

        public OrderDetailsController(DbOrderDetailsRpository orderDetailsRpository, DbOrderRepository dbOrderRepository)
        {
            _orderDetails = orderDetailsRpository;
            _orders = dbOrderRepository;
        }
        // GET: OrderDetails
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var model = new OrderDetailsViewModel();
            model.OrderDetails = _orderDetails.GetEntity(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(int id, OrderDetailsViewModel model)
        {
            if (model == null)
            {
                return HttpNotFound();
            }
            var updatedModel = _orderDetails.GetEntity(id);
            updatedModel.Amount = model.OrderDetails.Amount;
            updatedModel.TotalPrice = model.OrderDetails.Amount*updatedModel.UnitPrice;
            _orderDetails.UpdateEntity(updatedModel);

            var updatedOrder = _orders.GetEntity(updatedModel.OrderId);
            updatedOrder.Price= updatedOrder.OrderDetails.Sum(updateOrderOrderDetail => (int) updateOrderOrderDetail.TotalPrice);

            _orders.UpdateEntity(updatedOrder);
            return RedirectToAction("Index", "Order");
        }

        public ActionResult Delete(int id)
        {
            var model = _orderDetails.GetEntity(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View("Delete",model);
        }

        [HttpPost]
        public ActionResult Delete(OrderDetails model, int id)
        {
            if (model.Id != id)
            {
                ModelState.AddModelError("Name", "Bad Request");
                return View(model);
            }
            var updatedOrder = _orders.GetEntity(model.OrderId);
            updatedOrder.Price = updatedOrder.Price - (int)model.TotalPrice;
            _orders.UpdateEntity(updatedOrder);
            _orderDetails.DeleteEntity(id);
            return RedirectToAction("FilteredOrders", "Order", new {id= model.Order.Company.ParentCompanyId});
        }
    }
}