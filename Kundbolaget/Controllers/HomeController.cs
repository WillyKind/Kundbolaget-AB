using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kundbolaget.Controllers
{
    public class HomeController : Controller
    {
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
            return View();
        }
    }
}