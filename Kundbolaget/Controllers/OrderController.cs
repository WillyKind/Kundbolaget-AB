using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kundbolaget.EntityFramework.Repositories;
using Kundbolaget.PDFGenerator;

namespace Kundbolaget.Controllers
{
    public class OrderController : Controller
    {
        private DbOrderRepository _orders;

        public OrderController()
        {
            _orders = new DbOrderRepository();
        }
        // GET: Order
        public ActionResult Index()
        {
            var model = _orders.GetParentCompanies();
            return View(model);
        }

        public ActionResult FilteredOrders(int id)
        {
            var model = _orders.GetCompanyOrders(id);

            return View(model);
        }

        public ActionResult OrderDetails(int id, int companyId)
        {
            ViewBag.parentCompanyId = companyId;
            var model = _orders.GetOrderDetails(id);
            return View(model);
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

        public ActionResult OrderDetailsUnpickedOrder(int id)
        {
            var model = _orders.GetOrderDetails(id);
            return View(model);
        }

        public ActionResult OrderDetailsPickedOrder(int id)
        {
            var model = _orders.GetOrderDetails(id);
            return View(model);
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

        public ActionResult OrderDetailsShippedOrder(int id)
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

        public ActionResult GetOrderHistory()
        {
            var model = _orders.GetOrderHistory();
            return View(model);
        }

        public ActionResult OrderDetailsHistory(int id)
        {
            var model = _orders.GetOrderDetails(id);
            return View(model);
        }

    }
}