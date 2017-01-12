using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Kundbolaget.EntityFramework.Repositories;
using Kundbolaget.Models.EntityModels;
using Kundbolaget.ViewModels;

namespace Kundbolaget.Controllers
{
    public class ProductController : Controller
    {
        private readonly DbProductInfoRepository _productInfo;
        private readonly DbContainerRepository _containerRepository;
        private readonly DbProductGroupRepository _productGroupRepository;
        private readonly DbVolumeRepository _volumeRepository;

        public ProductController()
        {
            _productInfo = new DbProductInfoRepository();
            _containerRepository = new DbContainerRepository();
            _productGroupRepository = new DbProductGroupRepository();
            _volumeRepository = new DbVolumeRepository();
        }

        public ProductController(DbProductInfoRepository productInfoRepository,
            DbContainerRepository containerRepository, DbProductGroupRepository productGroupRepository,
            DbVolumeRepository volumeRepository)
        {
            _productInfo = productInfoRepository;
            _containerRepository = containerRepository;
            _productGroupRepository = productGroupRepository;
            _volumeRepository = volumeRepository;
        }

        // GET: Product
        public ActionResult Index()
        {
            return View("Index", _productInfo.GetEntities());
        }

        public ActionResult Edit(int id)
        {
            var model = new ManageProductInfosViewModel();
            model.ProductInfo = _productInfo.GetEntity(id);
            if (model.ProductInfo == null)
            {
                return HttpNotFound();
            }
            model.Containers = _containerRepository.GetEntities();
            model.ProductGroups = _productGroupRepository.GetEntities();
            model.Volumes = _volumeRepository.GetEntities();

            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(ManageProductInfosViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }
            _productInfo.UpdateEntity(model.ProductInfo);
            return RedirectToAction("Index", "Product");
        }

        public ActionResult Details(int id)
        {
            var model = _productInfo.GetEntity(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View("Details", model);
        }


        [HttpPost]
        public string Delete(int id)
        {
            var entity = _productInfo.GetEntity(id);
            entity.IsRemoved = true;
            _productInfo.UpdateEntity(entity);
            return "Success";
        }


        [HttpPost]
        public ActionResult Create(ManageProductInfosViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", model);
            }
            _productInfo.CreateEntity(model.ProductInfo);
            return RedirectToAction("Index", "Product");
        }

        public ActionResult Create()
        {
            var model = new ManageProductInfosViewModel();
            model.Containers = _containerRepository.GetEntities();
            model.ProductGroups = _productGroupRepository.GetEntities();
            model.Volumes = _volumeRepository.GetEntities();

            return View("Create", model);
        }
    }
}