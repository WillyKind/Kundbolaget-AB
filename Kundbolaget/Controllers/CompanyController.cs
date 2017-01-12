using System.Linq;
using System.Web.Mvc;
using Kundbolaget.EntityFramework.Repositories;
using Kundbolaget.Models.EntityModels;
using Kundbolaget.ViewModels;

namespace Kundbolaget.Controllers
{
    public class CompanyController : Controller
    {
        private readonly DbAddressRepository _addressRepository;
        private readonly DbCompanyRepository _companyRepository;
        private readonly DbContactPersonRepository _contactPersonRepository;
        private readonly DbCountryRepository _countryRepository;

        public CompanyController()
        {
            _companyRepository = new DbCompanyRepository();
            _addressRepository = new DbAddressRepository();
            _countryRepository = new DbCountryRepository();
            _contactPersonRepository = new DbContactPersonRepository();
        }

        public CompanyController(DbCompanyRepository companyRepository, DbAddressRepository addressRepository,
            DbCountryRepository countryRepository, DbContactPersonRepository contactPersonRepository)
        {
            _companyRepository = companyRepository;
            _addressRepository = addressRepository;
            _countryRepository = countryRepository;
            _contactPersonRepository = contactPersonRepository;
        }

        // GET: Company
        public ActionResult Index() => View(_companyRepository.GetEntities());

        [HttpPost]
        public string Delete(int id)
        {
            var entity = _companyRepository.GetEntity(id);
            entity.IsRemoved = true;
            _companyRepository.UpdateEntity(entity);
            return "Success";
        }

        [HttpPost]
        public string DeleteEntity(int id)
        {
            _companyRepository.DeleteEntity(id);
            return "Sucess";
        }

        public ActionResult Edit(int id)
        {
            var company = _companyRepository.GetEntity(id);
            if (company == null) return HttpNotFound();
            var addresses = _addressRepository.GetEntities();
            var countries = _countryRepository.GetEntities();
            var contactPersons = _contactPersonRepository.GetEntities();
            var parentCompanies = _companyRepository.GetParentCompanies(company.Id);

            var companyViewModel = new CompanyViewModel
            {
                Company = company,
                Addresses = addresses,
                ParentCompanies = parentCompanies,
                Countries = countries,
                ContactPersons = contactPersons
            };

            return View("Edit", companyViewModel);
        }

        [HttpPost]
        public ActionResult Edit(CompanyViewModel model)
        {
            if (!ModelState.IsValid)
                return View();
            _companyRepository.UpdateEntity(model.Company);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var company = _companyRepository.GetEntity(id);
            return company == null ? (ActionResult) HttpNotFound() : View(company);
        }

        public ActionResult Create()
        {
            var addresses = _addressRepository.GetEntities();
            var countries = _countryRepository.GetEntities();
            var contactPersons = _contactPersonRepository.GetEntities();
            var parentCompanies = _companyRepository.GetParentCompanies();

            var companyViewModel = new CompanyViewModel
            {
                Addresses = addresses,
                ParentCompanies = parentCompanies,
                Countries = countries,
                ContactPersons = contactPersons
            };

            return View("Create", companyViewModel);
        }

        [HttpPost]
        public ActionResult Create(CompanyViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            _companyRepository.CreateEntity(model.Company);
            return RedirectToAction("Index");
        }
    }
}