using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kundbolaget.EntityFramework.Repositories;
using Kundbolaget.Models.EntityModels;
using Kundbolaget.ViewModels;

namespace Kundbolaget.Controllers
{
    public class OrderController : Controller
    {
        private DbOrderRepository _orders;
        private DbCompanyRepository _companies;
        private DbInvoiceRepository _invoices;
        private DbProductStockRepository _productStock;


        public OrderController()
        {
            _orders = new DbOrderRepository();
            _companies = new DbCompanyRepository();
            _invoices = new DbInvoiceRepository();
            _productStock = new DbProductStockRepository();
        }

        //Constructor for tests
        public OrderController(DbOrderRepository dbOrderRepository, DbCompanyRepository dbCompanyRepository, DbInvoiceRepository dbInvoiceRepository, DbProductStockRepository dbProductStockRepository)
        {
            _orders = dbOrderRepository;
            _companies = dbCompanyRepository;
            _invoices = dbInvoiceRepository;
            _productStock = dbProductStockRepository;
        }

        // GET: Order
        public ActionResult Index()
        {
            var model = _companies.GetParentCompanies();
            return View(model);
        }

        public ActionResult Company(int id)
        {
            var model = new OrderViewModel();
            model.Orders = _orders.GetOrderHistoryForCompany(id);
            model.ParentCompanyId = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(OrderViewModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.Order.CreatedDate = DateTime.Now;
            _orders.CreateEntity(model.Order);
            return RedirectToAction("Delivery", "Home");
        }

        public ActionResult Create(int id, int customerOrderId, int companyId)
        {
            var model = new OrderViewModel();
            model.ParentCompanyId = id;
            model.Order = _orders.GetCompanyOrders(id).FirstOrDefault(o => o.CustomerOrderId == customerOrderId);
            model.Order.CustomerOrderId = customerOrderId;
            model.Order.CompanyId = companyId;
            model.Order.WishedDeliveryDate = DateTime.Now;
            return View("Create", model);
        }

        [HttpPost]
        public string Delete(int id)
        {
            var entity = _orders.GetEntity(id);
            RestoreProductStock(entity);
            entity.IsRemoved = true;
            _orders.UpdateEntity(entity);
            return "Success";
        }

        private void RestoreProductStock(Order entity)
        {
            foreach (var orderDetail in entity.OrderDetails)
            {
                var productStock = _productStock.GetEntity(orderDetail.ProductInfoId);
                productStock.Amount += orderDetail.ReservedAmount.Value;
                _productStock.UpdateEntity(productStock);
            }
        }

        public ActionResult GetAllUnpickedOrders()
        {
            var model = _orders.GetUnpickedOrders().OrderBy(d => d.CreatedDate).ThenBy(d => d.WishedDeliveryDate);
            return View(model);
        }


        [HttpPost]
        public string OrderPicked(int id)
        {
            var order = _orders.GetOrder(id);
            order.OrderPicked = DateTime.Now;
            _orders.UpdateOrder(order);
            return "Success " + id;
        }


        [HttpPost]
        public string OrderShipped(int id)
        {
            var order = _orders.GetOrder(id);
            order.OrderTransported = DateTime.Now;
            _orders.UpdateOrder(order);
            return "Success " + id;
        }

        public ActionResult GetPickedOrders()
        {
            var model = _orders.GetPickedOrders();
            return View(model);
        }

        public ActionResult GetShippedOrders()
        {
            var model = _orders.GetShippedOrders();
            return View(model);
        }

        public ActionResult OrderDetails(int id)
        {
            var model = _orders.GetOrderDetails(id);
            if (model.Length == 0)
            {
                return RedirectToAction("GetAllUnpickedOrders");
            }
            return View(model);
        }

        [HttpPost]
        public string OrderDelivered(int id)
        {
            var order = _orders.GetOrder(id);
            order.OrderDelivered = DateTime.Now;
            _orders.UpdateOrder(order);
            CreateInvoice(id);
            return "Success " + id;
        }


        private static void CalculatePallets(Order order)
        {
            var pallets = order.OrderDetails.Where(details => details.Amount >= 10).ToArray();
            if (pallets.Any())
            {
                foreach (var orderDetails in pallets)
                {
                    var remainder = orderDetails.Amount % 10;
                    var totalPallets = (orderDetails.Amount - remainder) / 10;
                }
            }
        }

        [HttpPost]
        public void SetComment(string comment, int id)
        {
            var order = _orders.GetOrder(id);
            order.Comment = comment;
            _orders.UpdateOrder(order);
        }

        public void CreateInvoice(int id)
        {
            var orderViewModel = new OrderViewModel
            {
                Order = _orders.GetEntity(id)
            };
            var invoice = new Invoice
            {
                Id = orderViewModel.Order.Id,
                Order = orderViewModel.Order,
                CreatedDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(90),
                InvoiceDetails = new List<InvoiceDetail>(),
                CompanyId = orderViewModel.Order.CompanyId,
                CustomerOrderId = orderViewModel.Order.CustomerOrderId,
                OriginalPrice = orderViewModel.Order.Price
            };


            foreach (var orderDetail in orderViewModel.Order.OrderDetails)
            {
                double finalPrice;

                if (orderDetail.Amount >= 10 && orderDetail.ProductInfo.PalletDiscount.HasValue)
                {
                    var remainder = orderDetail.Amount%10;
                    var totalPallets = (orderDetail.Amount - remainder)/10;
                    var remainderPrice = remainder*orderDetail.UnitPrice;
                    var palletPrice = totalPallets*10*orderDetail.UnitPrice;
                    var palletDiscount = palletPrice*orderDetail.ProductInfo.PalletDiscount.Value;
                    var discountedPrice = palletPrice - palletDiscount + remainderPrice;


                    finalPrice = discountedPrice;
                }
                else
                {
                    finalPrice = orderDetail.Amount*orderDetail.UnitPrice;
                }
                invoice.InvoiceDetails.Add(new InvoiceDetail
                {
                    FinalPrice = finalPrice,
                    InvoiceId = invoice.Id,
                    ProductInfo = orderDetail.ProductInfo
                });
                invoice.PriceWithPalletDiscount += (int) finalPrice;
            }


            invoice.PriceWithCompanyDiscount = invoice.PriceWithPalletDiscount / (invoice.Order.Company.ParentCompany.Discount + 1);
            orderViewModel.Order.Invoice = invoice;
            _orders.UpdateEntity(orderViewModel.Order);
        }

        [HttpPost]
        public string ExportDeliveryNote(int id)
        {
            PdfGenerator.PdfGenerator pdfGenerator = new PdfGenerator.PdfGenerator();
            pdfGenerator.ExportDeliveryNoteToPdf(id);
            return "Success " + id;
        }

        [HttpPost]
        public string ExportInvoice(int id)
        {
            PdfGenerator.PdfGenerator pdfGenerator = new PdfGenerator.PdfGenerator();
            pdfGenerator.ExportInvoiceToPdf(id);
            return "Success " + id;
        }
    }
}