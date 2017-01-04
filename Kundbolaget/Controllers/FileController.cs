using System;
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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Kundbolaget.Controllers
{
    public class FileController : Controller
    {
        private DbOrderRepository _orders = new DbOrderRepository();
        private DbCompanyRepository _companies = new DbCompanyRepository();
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

            string jsonSchema = @"{'$schema':'http://json-schema.org/draft-04/schema#','type':'object','properties':{'companyId':{'type':'string'},'customerOrderFileId':{'type':'integer'},'orders':{'type':'array','items':{'type':'object','properties':{'deliverTo':{'type':'string'},'deliverDate':{'type':'string'},'orderedProducts':{'type':'array','items':{'type':'object','properties':{'productId':{'type':'integer'},'amount':{'type':'integer'}},'required':['productId','amount']}}},'required':['deliverTo','deliverDate','orderedProducts']}}},'required':['companyId','customerOrderFileId','orders']}";
            var schema = JSchema.Parse(jsonSchema);
            var json = Encoding.Default.GetString(data);
            var jObj = JObject.Parse(json);
            if (!jObj.IsValid(schema))
            {
                return View(model);
            }

            var entity = JsonConvert.DeserializeObject<OrderFile>(json);
            var companyExists = _companies.ValidateCompanyId(int.Parse(entity.companyId));
            var orderExists = _orders.ValidateCompanyOrderId(entity.customerOrderFileId, int.Parse(entity.companyId));

            //check if companies in orderfile exist as childcompanies to parent company that placed order
            var subCompaniesExist = true;
            if (companyExists && !orderExists) {
                var childCompanies = _companies.GetChildCompanies(int.Parse(entity.companyId));
                foreach (var subOrder in entity.orders)
                {
                    if (childCompanies.Any(cc => cc.Id != int.Parse(subOrder.deliverTo)))
                    {
                        subCompaniesExist = false;
                        break;
                    }
                }
            }
            
           
            if (companyExists && !orderExists && subCompaniesExist)
            {
                _orders.CreateOrder(entity);
            }
            else {

                return View(model);
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