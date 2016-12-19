﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kundbolaget.EntityFramework.Repositories;
using Kundbolaget.Models.EntityModels;
using Kundbolaget.ViewModels;

namespace Kundbolaget.Controllers
{
    public class CompanyController : Controller
    {
        private readonly DbCompanyRepository _companyRepository;
        private readonly DbAddressRepository _addressRepository;
        private readonly DbCountryRepository _countryRepository;
        private readonly DbContactPersonRepository _contactPersonRepository;

        public CompanyController() {
            _companyRepository = new DbCompanyRepository();
            _addressRepository = new DbAddressRepository();
            _countryRepository = new DbCountryRepository();
            _contactPersonRepository = new DbContactPersonRepository();
        }

        public CompanyController(DbCompanyRepository companyRepository, DbAddressRepository addressRepository,
            DbCountryRepository countryRepository, DbContactPersonRepository contactPersonRepository) {
            _companyRepository = companyRepository;
            _addressRepository = addressRepository;
            _countryRepository = countryRepository;
            _contactPersonRepository = contactPersonRepository;
        }

        // GET: Company
        public ActionResult Index() {
            return View(_companyRepository.GetEntities());
        }

        public ActionResult Delete(int id) {
            var model = _companyRepository.GetEntity(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Company model, int id) {
            if (model.Id != id)
            {
                ModelState.AddModelError("Name", "Bad Request");
                return View(model);
            }
            _companyRepository.DeleteEntity(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id) {
            var model = _companyRepository.GetEntity(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            var addresses = _addressRepository.GetEntities();
            var countries = _countryRepository.GetEntities();
            var contactPersons = _contactPersonRepository.GetEntities();
            var parentCompanies = _companyRepository.GetEntities();

            var companyViewModel = new CompanyViewModel
            {
                Company = model,
                Addresses = addresses,
                ParentCompanies = parentCompanies,
                Countries = countries,
                ContactPersons = contactPersons
            };

            return View("Edit", companyViewModel);
        }

        [HttpPost]
        public ActionResult Edit(CompanyViewModel model) {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _companyRepository.UpdateEntity(model.Company);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id) {
            var model = _companyRepository.GetEntity(id);
            return View(model);
        }

        public ActionResult Create() {
            var addresses = _addressRepository.GetEntities();
            var countries = _countryRepository.GetEntities();
            var contactPersons = _contactPersonRepository.GetEntities();
            var parentCompanies = _companyRepository.GetEntities();

            var companyViewModel = new CompanyViewModel
            {
                Addresses = addresses,
                ParentCompanies = parentCompanies,
                Countries = countries,
                ContactPersons = contactPersons,
            };

            return View("Create", companyViewModel);
        }

        [HttpPost]
        public ActionResult Create(CompanyViewModel model) {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _companyRepository.CreateEntity(model.Company);
            return RedirectToAction("Index");
        }
    }
}