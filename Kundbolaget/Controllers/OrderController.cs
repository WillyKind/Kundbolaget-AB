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

        public ActionResult Company(int id)
        {
            var model = new OrderViewModel();
            model.Orders = _orders.GetCompanyOrders(id);
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
            return RedirectToAction("Index", "Order");
        }

        public ActionResult Create(int id)
        {
            var model = new OrderViewModel();
            model.ChildCompanies = _orders.GetChildCompanies(id);
            model.ParentCompanyId = id;
            return View("Create", model);
        }

        public ActionResult Delete(int id)
        {
            var model = _orders.GetEntity(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View("Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(ProductInfo model, int id)
        {
            if (model.Id != id)
            {
                ModelState.AddModelError("Name", "Bad Request");
                return View(model);
            }
            _orders.DeleteEntity(id);
            return RedirectToAction("Index");
        }
    }
}