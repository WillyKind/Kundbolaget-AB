using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kundbolaget.EntityFramework.Repositories;

namespace Kundbolaget.Controllers
{
    public class CustomerProductController : Controller
    {
        private DbProductInfoRepository _productInfo;
        

        public CustomerProductController()
        {
            _productInfo = new DbProductInfoRepository();
        }

        public CustomerProductController(DbProductInfoRepository productInfoRepository)
        {
            _productInfo = productInfoRepository;
        }
        // GET: CustomerProduct
        public ActionResult Index()
        {
            return View("Index", _productInfo.GetEntities());
        }
    }
}