using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kundbolaget.EntityFramework.Repositories;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.Controllers
{
    public class CompanyController : Controller
    {
        private DbCompanyRepository _companyRepository;

        public CompanyController()
        {
            _companyRepository = new DbCompanyRepository();
        }

        // GET: Company
        public ActionResult Index()
        {
            return View(_companyRepository.GetEntities());
        }

        public ActionResult Edit(int id)
        {
            var model = _companyRepository.GetEntity(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Company model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _companyRepository.UpdateEntity(model);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var model = _companyRepository.GetEntity(id);
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Company model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _companyRepository.CreateEntity(model);
            return RedirectToAction("Index");
        }
    }
}