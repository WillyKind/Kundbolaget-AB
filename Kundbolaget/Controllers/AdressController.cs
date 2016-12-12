using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kundbolaget.EntityFramework.Repositories;

namespace Kundbolaget.Controllers
{
    public class AdressController : Controller
    {
        private DbAdressRepository _adressRepository;

        public AdressController()
        {
            _adressRepository = new DbAdressRepository();
        }
        // GET: Adress
        public ActionResult Index()
        {
            return View(_adressRepository.GetEntities());
        }
    }
}