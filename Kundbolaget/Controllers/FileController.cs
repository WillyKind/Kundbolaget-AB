﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.EntityFramework.Repositories;
using Kundbolaget.JsonEntityModels;
using Kundbolaget.Models;
using Kundbolaget.ViewModels;
using Newtonsoft.Json;

namespace Kundbolaget.Controllers
{
    public class FileController : Controller
    {
        private DbOrderRepository _orders = new DbOrderRepository();
        // GET: File
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UploadJson()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadJson(FileUploadVM model)
        {
            if (model.File == null || model.File.FileName.Substring(Math.Max(0, model.File.FileName.Length - 4)) != "json")
                return View(model);

            byte[] data;

            using (var ms = new MemoryStream())
            {
                model.File.InputStream.CopyTo(ms);
                data = ms.ToArray();
            }
            var json = Encoding.Default.GetString(data);
            var entity = JsonConvert.DeserializeObject<OrderFile>(json);
            _orders = new DbOrderRepository();
            var companyExists = _orders.ValidateCompanyId(int.Parse(entity.companyId));
            var orderExists = _orders.ValidateCompanyOrderId(entity.customerOrderFileId, int.Parse(entity.companyId));

            if (companyExists && !orderExists)
            {
                _orders.CreateOrder(entity);
            }

            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            _orders.Dispose();
            base.Dispose(disposing);
        }
    }
}