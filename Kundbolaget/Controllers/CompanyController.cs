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
        private DbAddressRepository _addressRepository;
        private DbCountryRepository _countryRepository;
        private DbContactPersonRepository _contactPersonRepository;
        private DbParentCompanyRepository _parentCompanyRepository;
        private DbDeliveryAddressRepository _dbDeliveryAddressRepository;


        public CompanyController()
        {
            _companyRepository = new DbCompanyRepository();
            _addressRepository = new DbAddressRepository();
            _countryRepository = new DbCountryRepository();
            _contactPersonRepository = new DbContactPersonRepository();
            _parentCompanyRepository = new DbParentCompanyRepository();
            _dbDeliveryAddressRepository = new DbDeliveryAddressRepository();
        }

        // GET: Company
        public ActionResult Index()
        {
            return View(_companyRepository.GetEntities());
        }

        public ActionResult Edit(int id)
        {
            var model = _companyRepository.GetEntity(id);
            var addresses = _addressRepository.GetEntities();
            var countries = _countryRepository.GetEntities();

            var selectListAddresses = addresses.Select(address => new SelectListItem
            {
                Value = address.Id.ToString(),
                Text = address.Street
            }).ToList();

            var selectListCountries = countries.Select(country => new SelectListItem
            {
                Value = country.Id.ToString(),
                Text = country.Name
            }).ToList();

            ViewBag.Addresses = selectListAddresses;
            ViewBag.Countries = selectListCountries;

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
            //var addresses = _addressRepository.GetEntities();
            var countries = _countryRepository.GetEntities();
            var parentCompanies = _parentCompanyRepository.GetEntities();

            //var selectListAddresses = addresses.Select(address => new SelectListItem
            //{
            //    Value = address.Id.ToString(),
            //    Text = address.Street
            //}).ToList();

            var selectListCountries = countries.Select(country => new SelectListItem
            {
                Value = country.Id.ToString(),
                Text = country.Name
            }).ToList();

            var selectListParentCompanies = parentCompanies.Select(parentCompany => new SelectListItem
            {
                Value = parentCompany.Id.ToString(),
                Text = parentCompany.Name,
            }).ToList();
            selectListParentCompanies.Add(new SelectListItem {Value = "NULL", Text = "Inget"});
            //ViewBag.Addresses = selectListAddresses;
            ViewBag.Countries = selectListCountries;
            ViewBag.ParentCompanies = selectListParentCompanies;
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