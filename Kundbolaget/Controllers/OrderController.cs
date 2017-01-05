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
    public class OrderController : Controller
    {
        private DbOrderRepository _orders;
        private DbCompanyRepository _companies;

        public OrderController()
        {
            _orders = new DbOrderRepository();
            _companies = new DbCompanyRepository();
        }

        //Constructor for tests
        public OrderController(DbOrderRepository dbOrderRepository)
        {
            _orders = dbOrderRepository;
        }

        // GET: Order
        public ActionResult Index()
        {
            var model = _companies.GetParentCompanies();
            return View(model);
        }

        public ActionResult Company(int id)
        {
            var model = new OrderViewModel();
            model.Orders = _orders.GetOrderHistoryForCompany(id);
            model.ParentCompanyId = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(OrderViewModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.Order.CreatedDate = DateTime.Now;
            _orders.CreateEntity(model.Order);
            return RedirectToAction("Delivery", "Home");
        }

        public ActionResult Create(int id, int customerOrderId, int companyId)
        {
            var model = new OrderViewModel();
            model.ParentCompanyId = id;
            model.Order = _orders.GetCompanyOrders(id).FirstOrDefault(o => o.CustomerOrderId == customerOrderId);
            model.Order.CustomerOrderId = customerOrderId;
            model.Order.CompanyId = companyId;
            model.Order.WishedDeliveryDate = DateTime.Now;
            return View("Create", model);
        }

        [HttpPost]
        public string Delete(int id)
        {
            var entity = _orders.GetEntity(id);
            entity.IsRemoved = true;
            _orders.UpdateEntity(entity);
            return "Success";
        }

        public ActionResult GetAllUnpickedOrders()
        {
            var model = _orders.GetUnpickedOrders();
            return View(model);
        }


        [HttpPost]
        public string OrderPicked(int id)
        {
            var order = _orders.GetOrder(id);
            order.OrderPicked = DateTime.Now;
            _orders.UpdateOrder(order);
            return "Success " + id;
        }


        [HttpPost]
        public string OrderShipped(int id)
        {
            var order = _orders.GetOrder(id);
            order.OrderTransported = DateTime.Now;
            _orders.UpdateOrder(order);
            return "Success " + id;
        }

        public ActionResult GetPickedOrders()
        {
            var model = _orders.GetPickedOrders();
            return View(model);
        }

        public ActionResult GetShippedOrders()
        {
            var model = _orders.GetShippedOrders();
            return View(model);
        }

        public ActionResult OrderDetails(int id)
        {
            var model = _orders.GetOrderDetails(id);
            return View(model);
        }

        [HttpPost]
        public string OrderDelivered(int id)
        {
            var order = _orders.GetOrder(id);
            order.OrderDelivered = DateTime.Now;
            _orders.UpdateOrder(order);
            return "Success " + id;
        }

        private static void CalculatePallets(Order order)
        {
            var pallets = order.OrderDetails.Where(details => details.Amount >= 10).ToArray();
            if (pallets.Any())
            {
                foreach (var orderDetails in pallets)
                {
                    var remainder = orderDetails.Amount % 10;
                    var totalPallets = (orderDetails.Amount - remainder) / 10;
                }
            }
        }

        [HttpPost]
        public void SetComment(string comment, int id)
        {
            var order = _orders.GetOrder(id);
            order.Comment = comment;
            _orders.UpdateOrder(order);
        }
    }
}