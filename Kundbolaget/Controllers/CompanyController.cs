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
        private readonly DbCompanyRepository _companyRepository;
        private readonly DbAddressRepository _addressRepository;
        private readonly DbCountryRepository _countryRepository;
        private readonly DbContactPersonRepository _contactPersonRepository;
        
        public CompanyController()
        {
            _companyRepository = new DbCompanyRepository();
            _addressRepository = new DbAddressRepository();
            _countryRepository = new DbCountryRepository();
            _contactPersonRepository = new DbContactPersonRepository();
        }

        // GET: Company
        public ActionResult Index()
        {
            return View(_companyRepository.GetEntities());
        }

        public ActionResult Delete(int id)
        {
            var model = _companyRepository.GetEntity(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(Company model, int id)
        {
            if (model.Id != id)
            {
                ModelState.AddModelError("Name", "Bad Request");
                return View(model);
            }
            _companyRepository.DeleteEntity(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var model = _companyRepository.GetEntity(id);
            var addresses = _addressRepository.GetEntities();
            var countries = _countryRepository.GetEntities();
            var contactPersons = _contactPersonRepository.GetEntities();
            var parentCompanies = _companyRepository.GetEntities();
            
            var selectListAddresses = addresses.Select(address => new SelectListItem
            {
                Value = address.Id.ToString(),
                Text = address.Street + " " + address.Number
            }).ToList();

            var selectListCountries = countries.Select(country => new SelectListItem
            {
                Value = country.Id.ToString(),
                Text = country.Name
            }).ToList();

            var selectListContactPersons = contactPersons.Select(contactPerson => new SelectListItem
            {
                Value = contactPerson.Id.ToString(),
                Text = contactPerson.FirstName + " " + contactPerson.LastName
            }).ToList();

            var selectListParentCompanies = parentCompanies.Where(pCompany => pCompany.ParentCompanyId == null).Select(pCompany=> new SelectListItem
            {
                Value = pCompany.Id.ToString(),
                Text = pCompany.Name
            }).ToList();

            selectListParentCompanies.Add(new SelectListItem { Value = "", Text = "Inget" });

            ViewBag.Addresses = selectListAddresses;
            ViewBag.Countries = selectListCountries;
            ViewBag.ContactPersons = selectListContactPersons;
            ViewBag.ParentCompanies = selectListParentCompanies;
            
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
            var addresses = _addressRepository.GetEntities();
            var countries = _countryRepository.GetEntities();
            var contactPersons = _contactPersonRepository.GetEntities();
            var parentCompanies = _companyRepository.GetEntities();
            
            var selectListAddresses = addresses.Select(address => new SelectListItem
            {
                Value = address.Id.ToString(),
                Text = address.Street + " " + address.Number
            }).ToList();

            var selectListCountries = countries.Select(country => new SelectListItem
            {
                Value = country.Id.ToString(),
                Text = country.Name
            }).ToList();

            var selectListContactPersons = contactPersons.Select(contactPerson => new SelectListItem
            {
                Value = contactPerson.Id.ToString(),
                Text = contactPerson.FirstName + " " + contactPerson.LastName
            }).ToList();

            var selectListParentCompanies = parentCompanies.Where(pCompany => pCompany.ParentCompanyId == null).Select(pCompany => new SelectListItem
            {
                Value = pCompany.Id.ToString(),
                Text = pCompany.Name
            }).ToList();

            selectListParentCompanies.Add(new SelectListItem {Value = "", Text = "Inget"});

            ViewBag.Addresses = selectListAddresses;
            ViewBag.Countries = selectListCountries;
            ViewBag.ContactPersons = selectListContactPersons;
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