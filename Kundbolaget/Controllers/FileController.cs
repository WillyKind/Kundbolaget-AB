using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.EntityFramework.Repositories;
using Kundbolaget.JsonEntityModels;
using Kundbolaget.Models;
using Kundbolaget.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Kundbolaget.Controllers
{
    public class FileController : Controller
    {
        private DbOrderRepository _orders = new DbOrderRepository();
        private DbCompanyRepository _companies = new DbCompanyRepository();
        private DbProductInfoRepository _products = new DbProductInfoRepository();
        private ErrorViewModel _errorViewModel = new ErrorViewModel();
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
        public ActionResult UploadJson(FileUploadViewModel model)
        {
            //check if file ends with ".json"
            if (model.File == null ||
                model.File.FileName.Substring(Math.Max(0, model.File.FileName.Length - 4)) != "json")
                return View(model);

            byte[] data;

            using (var ms = new MemoryStream())
            {
                model.File.InputStream.CopyTo(ms);
                data = ms.ToArray();
            }

            string jsonSchema =
                @"{'$schema':'http://json-schema.org/draft-04/schema#','type':'object','properties':{'companyId':{'type':'string'},'customerOrderFileId':{'type':'integer'},'orders':{'type':'array','items':{'type':'object','properties':{'deliverTo':{'type':'string'},'deliverDate':{'type':'string'},'orderedProducts':{'type':'array','items':{'type':'object','properties':{'productId':{'type':'integer'},'amount':{'type':'integer'}},'required':['productId','amount']}}},'required':['deliverTo','deliverDate','orderedProducts']}}},'required':['companyId','customerOrderFileId','orders']}";
            var schema = JSchema.Parse(jsonSchema);
            var json = Encoding.Default.GetString(data);
            var jObj = JObject.Parse(json);
            if (!jObj.IsValid(schema))
            {
                return View(model);
            }

            var entity = JsonConvert.DeserializeObject<OrderFile>(json);
            //check if company that places order is a company in database
            var company = _companies.ValidateCompanyId(int.Parse(entity.companyId));
            if (company == null)
            {
                _errorViewModel.Message = "Det moderbolag ni angett är ej giltigt.";
                return View("OrderFileError", _errorViewModel);
            }

            //check if companies in orderfile exist as childcompanies to parent company that placed order
            var subCompaniesExist =
                entity.orders.All(subOrder => company.SubCompanies.Any(cc => cc.Id == int.Parse(subOrder.deliverTo)));
            if (!subCompaniesExist)
            {
                _errorViewModel.Message = "En eller flera underföretag ni angett existerar ej som kund.";
                return View("OrderFileError", _errorViewModel);
            }
            //check if ordered product exists in database
            var products = _products.GetEntities().ToDictionary(p => p.Id);
            var productsExists =
                entity.orders.All(so => so.orderedProducts.All(product => products.ContainsKey(product.productId)));
            if (!productsExists)
            {
                _errorViewModel.Message = "En eller flera av de produkter ni försöker beställa finns ej.";
                return View("OrderFileError", _errorViewModel);
            }
            //check if order has been registered in database before
            var orderExists = _orders.ValidateCompanyOrderId(entity.customerOrderFileId, int.Parse(entity.companyId));
            if (orderExists)
            {
                _errorViewModel.Message =
                    "Denna order har redan registrerats i vår databas, vänligen kontrollera ert referensnummer.";
                return View("OrderFileError", _errorViewModel);
            }

            //check that datetime has not passed for order
            var datePassed = entity.orders.Any(o => DateTime.Parse(o.deliverDate) < DateTime.Now);
            if (datePassed)
            {
                _errorViewModel.Message =
                    "Denna order innehåller önskade leveransdatum som redan har passerat.";
                return View("OrderFileError", _errorViewModel);
            }
            _orders.CreateOrder(entity);
            model.OrderFile = entity;

            return View("OrderFileSuccess", model);
        }


        protected override void Dispose(bool disposing)
        {
            _orders.Dispose();
            base.Dispose(disposing);
        }
    }
}