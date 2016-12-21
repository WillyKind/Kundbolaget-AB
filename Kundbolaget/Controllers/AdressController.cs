using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kundbolaget.EntityFramework.Repositories;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.Controllers
{
    public class AddressController : Controller
    {
        private readonly DbAddressRepository _adressRepository;

        public AddressController()
        {
            _adressRepository = new DbAddressRepository();
        }

        public AddressController(DbAddressRepository dbAddressRepository)
        {
            _adressRepository = dbAddressRepository;
        }

        public ActionResult Index()
        {
            return View("Index", _adressRepository.GetEntities());
        }

        public ActionResult Edit(int id)
        {
            var model = _adressRepository.GetEntity(id);
            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(Address model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _adressRepository.UpdateEntity(model);
            return RedirectToAction("Index", "Address");
        }

        public ActionResult Delete(int id)
        {
            var model = _adressRepository.GetEntity(id);
            return View("Delete",model);
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
            return RedirectToAction("Index", "Address");
        }

        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(Address model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _adressRepository.CreateEntity(model);
            return RedirectToAction("Index", "Address");
        }

        public ActionResult Details(int id)
        {
            var model = _adressRepository.GetEntity(id);
            return View("Details",model);
        }
    }
}