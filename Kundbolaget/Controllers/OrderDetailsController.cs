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
            //ViewBag.parentCompanyId = companyId;
            var model = new OrderDetailsViewModel();
            model.OrderDetailses = _orders.GetOrderDetails(id);
            model.OrderId = id;
            model.ParentCompanyId = _orders.GetEntity(id).Company.ParentCompany.Id;

            return View(model);
        }

        public ActionResult Create(int id)
        {
            var model = new OrderDetailsViewModel();
            model.ProductInfos = _products.GetEntities();
            model.OrderDetails.OrderId = id;

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
            var updatedOrder = _orders.GetEntity(model.OrderDetails.OrderId);
            updatedOrder.Price +=(int) model.OrderDetails.TotalPrice;

            _orders.UpdateEntity(updatedOrder);
            _orderDetails.CreateEntity(model.OrderDetails);
            return RedirectToAction("Create","OrderDetails",new {id=updatedOrder.Id});
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
            return RedirectToAction("Index", new {id=updatedOrder.Id, companyId= updatedOrder.Company.ParentCompanyId});
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
            return RedirectToAction("Index", new {id=updatedOrder.Id,companyId= model.Order.Company.ParentCompanyId});
        }
    }
}