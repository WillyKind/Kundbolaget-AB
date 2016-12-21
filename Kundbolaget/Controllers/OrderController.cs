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
    }
}