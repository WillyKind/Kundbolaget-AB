using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kundbolaget.EntityFramework.Repositories;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.Controllers
{
    public class AdressController : Controller
    {
        private DbAdressRepository _adressRepository;

        public AdressController()
        {
            _adressRepository = new DbAdressRepository();
        }
        
        public ActionResult Index()
        {
            return View(_adressRepository.GetEntities());
        }

        public ActionResult Edit(int id)
        {
            var model = _adressRepository.GetEntity(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Address model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _adressRepository.UpdateEntity(model);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var model = _adressRepository.GetEntity(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Address model, int id)
        {
            if (model.Id != id)
            {
                ModelState.AddModelError("Name", "Bad Request");
                return View(model);
            }
            _adressRepository.DeleteEntity(id);
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Address model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _adressRepository.CreateEntity(model);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var model = _adressRepository.GetEntity(id);
            return View(model);
        }
    }
}