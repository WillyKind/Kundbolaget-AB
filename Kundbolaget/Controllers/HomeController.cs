using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kundbolaget.EntityFramework.Repositories;

namespace Kundbolaget.Controllers
{
    public class HomeController : Controller
    {
        DbOrderRepository _orders = new DbOrderRepository();
        DbProductStockRepository _stock = new DbProductStockRepository();
        
        // GET: Home
        public ActionResult Index()
        {
            return View("Index");
        }
        public ActionResult AdminManager()
        {
            return View("AdminManager");
        }
        public ActionResult CustomerSite()
        {
            return View("CustomerSite");
        }

        public ActionResult Delivery()
        {
            var unpickedOrders = _orders.GetUnpickedOrders().ToList();
            _orders.AmountDiffSolver(unpickedOrders);
            _orders.IsOrderComplete(unpickedOrders);

            return View();
        }
    }
}