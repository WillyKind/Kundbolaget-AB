using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Kundbolaget.EntityFramework.Repositories;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.Controllers
{
    public class ProductController : Controller
    {
        private DbProductInfoRepository _productInfo;
        private DbContainerRepository _containerRepository;
        private DbProductGroupRepository _productGroupRepository;

        public ProductController()
        {
            _productInfo = new DbProductInfoRepository();
            _containerRepository = new DbContainerRepository();
            _productGroupRepository = new DbProductGroupRepository();
        }

        // GET: Product
        public ActionResult Index()
        {
            return View(_productInfo.GetEntities());
        }

        public ActionResult Edit(int id)
        {
            var model = _productInfo.GetEntity(id);
            var containers = _containerRepository.GetEntities();
            var productGroups = _productGroupRepository.GetEntities();

            var selectListContainers = containers.Select(container => new SelectListItem
            {
                Value = container.Id.ToString(),
                Text = container.Name
            }).ToList();

            var selectListProductGroups = productGroups.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            }).ToList();

            ViewBag.Containers = selectListContainers;
            ViewBag.ProductGroups = selectListProductGroups;

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProductInfo model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _productInfo.UpdateEntity(model);
            return RedirectToAction("Index");
        }
    }
}