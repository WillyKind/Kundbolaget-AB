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
            var model = _orders.GetParentCompanies();
            return View(model);
        }

        public ActionResult FilteredOrders(int id)
        {
            var model = _orders.GetCompanyOrders(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.Order.CreatedDate = DateTime.Now;
            model.Order.CompanyId = model.CustomerId;
            _orders.CreateEntity(model.Order);
            return RedirectToAction("Index", "Order");
        }

        public ActionResult Create(int id)
        {
            var model = new OrderViewModel();
            model.CustomerId = id;
            return View("Create", model);
        }

        public ActionResult OrderDetails(int id, int companyId)
        {
            ViewBag.parentCompanyId = companyId;
            var model = new OrderDetailsViewModel(); 
            model.OrderDetailses = _orders.GetOrderDetails(id);
            model.OrderId = id; 
            
            return View(model);
        }
    }
}