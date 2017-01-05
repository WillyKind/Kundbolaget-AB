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
            _orders = new DbOrderRepository();
            var companyExists = _companies.ValidateCompanyId(int.Parse(entity.companyId));
            var orderExists = _orders.ValidateCompanyOrderId(entity.customerOrderFileId, int.Parse(entity.companyId));

            if (companyExists && !orderExists)
            {
                var orders = _orders.CreateOrder(entity);
                foreach (var order in orders)
                {
                    foreach (var details in order.OrderDetails)
                    {
                        foreach (var productInfoProductStock in details.ProductInfo.ProductStocks)
                        {
                            if (productInfoProductStock.Amount >= details.Amount)
                            {
                                details.ReservedAmount = details.Amount;
                                productInfoProductStock.Amount -= details.Amount;
                            }
                        }
                    }
                   
                }
                    foreach (var order1 in orders)
                    {
                        _orders.UpdateOrder(order1);
                    }
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