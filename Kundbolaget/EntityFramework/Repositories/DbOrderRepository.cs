using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Web;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.Interfaces;
using Kundbolaget.JsonEntityModels;
using Kundbolaget.Models;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.EntityFramework.Repositories
{
    public class DbOrderRepository
    {
        private StoreContext db;

        public void Dispose()
        {
            db.Dispose();
        }

        public DbOrderRepository()
        {
            db = new StoreContext();
        }
        public DbOrderRepository(StoreContext fakeContext)
        {
            db = fakeContext;
        }
        public bool ValidateCompanyId(int id)
        {
            var companyExists = db.Companies.Any(c => c.Id == id && !c.IsRemoved);
            return companyExists;
        }

        public bool ValidateCompanyOrderId(int customerOrderId, int companyId)
        {
            var ordernumerExists =
                db.Orders.Where(o => o.Company.ParentCompanyId == companyId && !o.IsRemoved)
                    .Any(o => o.CustomerOrderId == customerOrderId);
            return ordernumerExists;
        }

        public void CreateEntity(Order newEntity)
        {
            db.Orders.Add(newEntity);
            db.SaveChanges();
        }

        public Order GetEntity(int id)
        {
            return db.Orders.FirstOrDefault(o => o.Id == id);
        }

        public void UpdateEntity(Order updatedEntity)
        {
            db.Orders.Attach(updatedEntity);
            var entry = db.Entry(updatedEntity);
            entry.State = EntityState.Modified;
            db.SaveChanges();
        }

        public void CreateOrder(OrderFile order)
        {
            var orders = new List<Order>();
            foreach (var subOrder in order.orders)
            {
                var orderedProducts = new List<OrderDetails>();
                foreach (var products in subOrder.orderedProducts)
                {
                    var product = db.ProductsInfoes.FirstOrDefault(p => p.Id == products.productId);
                    orderedProducts.Add(new OrderDetails
                    {
                        ProductInfo = product,
                        ProductInfoId = products.productId,
                        Amount = products.amount,
                        UnitPrice = product.Price,
                        TotalPrice = product.Price * products.amount
                    });
                }
                orders.Add(new Order
                {
                    CompanyId = int.Parse(subOrder.deliverTo),
                    WishedDeliveryDate = DateTime.Parse(subOrder.deliverDate),
                    CreatedDate = DateTime.Now,
                    CustomerOrderId = order.customerOrderFileId,
                    OrderDetails = orderedProducts,
                    Price = orderedProducts.Sum(p => p.ProductInfo.Price*p.Amount)
                });
            }
            db.Orders.AddRange(orders);
            db.SaveChanges();
        }
        public void DeleteEntity(int id)
        {
            var product = db.Orders.SingleOrDefault(p => p.Id == id);
            if (product != null)
            {
                product.IsRemoved = true;
                db.SaveChanges();
            }
        }

        public Company[] GetChildCompanies(int id)
        {
            return db.Companies.Where(c => c.ParentCompanyId == id && !c.IsRemoved).ToArray();
        }
        public Company[] GetParentCompanies()
        {
            return db.Companies.Where(c => c.ParentCompany == null && !c.IsRemoved).ToArray();
        }

        public Order[] GetCompanyOrders(int id)
        {
            return db.Orders.Where(o => o.Company.ParentCompanyId == id && !o.IsRemoved).ToArray();
        }

        public OrderDetails[] GetOrderDetails(int id)
        {
            return db.OrderDetails.Where(o => o.OrderId == id).ToArray();
        }
    }
}