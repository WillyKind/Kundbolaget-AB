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
        private readonly IEntityRepository<ProductStock> _stockRepository;

        public ProductStockController()
        {
            _stockRepository = new DbProductStockRepository();
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
            var productinfoSelectListItems = new DbProductInfoRepository().GetEntities().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            var warehouseSelectListItems = new DbWarehouseRepository().GetEntities().Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();

            ViewBag.ProductInfoes = productinfoSelectListItems;
            ViewBag.Warehouses = warehouseSelectListItems;
            return View();
        }
    }
}