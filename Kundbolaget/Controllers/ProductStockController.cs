using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Kundbolaget.EntityFramework.Repositories;
using Kundbolaget.Interfaces;
using Kundbolaget.Models.EntityModels;
using Kundbolaget.ViewModels;

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
            var model = new ProductStockVM();
            model.ProductInfos = _productInfoRepository.GetEntities();
            model.Warehouses = _warehouseRepository.GetEntities();
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var stockVm = new ProductStockVM();

            stockVm.ProductInfos = _productInfoRepository.GetEntities();
            stockVm.Warehouses = _warehouseRepository.GetEntities();
            stockVm.ProductStock = _stockRepository.GetEntity(id);
            if (stockVm.ProductStock == null)
            {
                return HttpNotFound();
            }
            return View(stockVm);
        }

        [HttpPost]
        public ActionResult Edit(ProductStockVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _stockRepository.UpdateEntity(model.ProductStock);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            return View(_stockRepository.GetEntity(id));
        }
    }
}