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
            var model = _productInfo.GetEntity(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            var containers = _containerRepository.GetEntities();
            var productGroups = _productGroupRepository.GetEntities();
            var volumes = _volumeRepository.GetEntities();

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

            var selectListVolumes = volumes.Select(volume => new SelectListItem
            {
                Value = volume.Id.ToString(),
                Text = volume.Milliliter.ToString()
            }).ToList();

            ViewBag.Containers = selectListContainers;
            ViewBag.ProductGroups = selectListProductGroups;
            ViewBag.Volume = selectListVolumes;

            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(ProductInfo model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _productInfo.UpdateEntity(model);
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

        public ActionResult Delete(int id)
        {
            var model = _productInfo.GetEntity(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            return View("Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(ProductInfo model, int id)
        {
            if (model.Id != id)
            {
                ModelState.AddModelError("Name", "Bad Request");
                return View(model);
            }
            _productInfo.DeleteEntity(id);
            return RedirectToAction("Index", "Product");
        }

        [HttpPost]
        public ActionResult Create(ProductInfo model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _productInfo.CreateEntity(model);
            return RedirectToAction("Index", "Product");
        }

        public ActionResult Create()
        {
            var containers = _containerRepository.GetEntities();
            var productGroups = _productGroupRepository.GetEntities();
            var volumes = _volumeRepository.GetEntities();

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

            var selectListVolumes = volumes.Select(volume => new SelectListItem
            {
                Value = volume.Id.ToString(),
                Text = volume.Milliliter.ToString()
            }).ToList();

            ViewBag.Containers = selectListContainers;
            ViewBag.ProductGroups = selectListProductGroups;
            ViewBag.Volume = selectListVolumes;

            return View("Create");
        }
    }
}