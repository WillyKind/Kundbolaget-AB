using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Kundbolaget.EntityFramework.Repositories;
using Kundbolaget.Interfaces;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.Controllers
{
    public class ProductStockController : Controller
    {
        private readonly DbProductStockRepository _stockRepository;
        private readonly DbProductInfoRepository _productInfoRepository;
        private readonly DbWarehouseRepository _warehouseRepository;

        public ProductStockController()
        {
            _stockRepository = new DbProductStockRepository();
            _productInfoRepository = new DbProductInfoRepository();
            _warehouseRepository = new DbWarehouseRepository();
        }

        public ProductStockController(DbProductStockRepository dbProductStockRepository, DbProductInfoRepository dbProductInfoRepository, DbWarehouseRepository dbWarehouseRepository)
        {
            _stockRepository = dbProductStockRepository;
            _productInfoRepository = dbProductInfoRepository;
            _warehouseRepository = dbWarehouseRepository;

        }

        // GET: ProductStock
        public ActionResult Index()
        {
            return View(_stockRepository.GetEntities());
        }

        [HttpPost]
        public ActionResult Create(ProductStock productStock)
        {
            if (!ModelState.IsValid)
            {
                return View(productStock);
            }
            _stockRepository.CreateEntity(productStock);
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            var productinfoSelectListItems = _productInfoRepository.GetEntities().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            var warehouseSelectListItems = _warehouseRepository.GetEntities().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            ViewBag.ProductInfoes = productinfoSelectListItems;
            ViewBag.Warehouses = warehouseSelectListItems;
            return View();
        }

        public ActionResult Edit(int id)
        {


            var productinfoSelectListItems = _productInfoRepository.GetEntities().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            var warehouseSelectListItems = _warehouseRepository.GetEntities().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            ViewBag.ProductInfoes = productinfoSelectListItems;
            ViewBag.Warehouses = warehouseSelectListItems;

            return View(_stockRepository.GetEntity(id));
        }

        [HttpPost]
        public ActionResult Edit(ProductStock model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _stockRepository.UpdateEntity(model);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            return View(_stockRepository.GetEntity(id));
        }
    }
}