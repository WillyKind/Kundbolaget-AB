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

        public List<Order> CreateOrder(OrderFile order)
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
                        TotalPrice = product.Price * products.amount,
                    });
                }
                orders.Add(new Order
                {
                    CompanyId = subOrder.deliverTo,
                    WishedDeliveryDate = DateTime.Parse(subOrder.deliverDate),
                    CreatedDate = DateTime.Now,
                    CustomerOrderId = order.customerOrderFileId,
                    OrderDetails = orderedProducts,
                    Price = orderedProducts.Sum(p => p.ProductInfo.Price*p.Amount)
                });
            }
            db.Orders.AddRange(orders);
            db.SaveChanges();
            return orders;
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


        public Order[] GetCompanyOrders(int id)
        {
            return db.Orders.Where(o => o.Company.ParentCompanyId == id && !o.IsRemoved).ToArray();
        }

        public OrderDetails[] GetOrderDetails(int id)
        {
            return db.OrderDetails.Where(o => o.OrderId == id).ToArray();
        }

        public Order[] GetUnpickedOrders()
        {
            return db.Orders.Where(o => !o.IsRemoved && o.OrderPicked == null).ToArray();
        }

        public Order GetOrder(int orderId)
        {
            return db.Orders.FirstOrDefault(o => o.Id == orderId);
        }

        public void UpdateOrder(Order order)
        {
            db.Orders.Attach(order);
            var entry = db.Entry(order);
            entry.State = EntityState.Modified;
            db.SaveChanges();
        }

        public void UpdateOrder(List<Order> orders)
        {
            foreach (var order in orders)
            {
                db.Orders.Attach(order);
                var entry = db.Entry(order);
                entry.State = EntityState.Modified;
            }
            db.SaveChanges();
        }


        public Order[] GetPickedOrders()
        {
            return db.Orders.Where(o => !o.IsRemoved && o.OrderTransported == null && o.OrderPicked != null).ToArray();
        }

        public Order[] GetShippedOrders()
        {
            return db.Orders.Where(o => !o.IsRemoved &&
                                        o.OrderTransported != null &&
                                        o.OrderPicked != null &&
                                        o.OrderDelivered == null).ToArray();
        }

        public Order[] GetOrderHistory()
        {
            return db.Orders.Where(o => !o.IsRemoved &&
                                       o.OrderTransported != null &&
                                       o.OrderPicked != null &&
                                       o.OrderDelivered != null).ToArray();
        }

        public Order[] GetOrderHistoryForCompany(int id)
        {
            return db.Orders.Where(o => o.Company.ParentCompany.Id==id &&
                                       !o.IsRemoved &&
                                       o.OrderTransported != null &&
                                       o.OrderPicked != null &&
                                       o.OrderDelivered != null).ToArray();
        }

        public void AllocateProducts(List<Order> orders)
        {
            var orderDetails = orders.SelectMany(x => x.OrderDetails);
            foreach (var detail in orderDetails)
            {
                foreach (var stock in detail.ProductInfo.ProductStocks)
                {
                    if (stock.Amount >= detail.Amount)
                    {
                        detail.ReservedAmount = detail.Amount;
                        stock.Amount -= detail.Amount;
                    }
                    else if (stock.Amount >= 0 && detail.Amount > stock.Amount)
                    {
                        detail.ReservedAmount = stock.Amount;
                        stock.Amount = 0;
                    }
                }
            }
            UpdateOrder(orders);
        }

        public  List<ProductStock> AmountDiffSolver(List<Order> orders)
        {
            var productStocks = new List<ProductStock>();

            foreach (var order in orders)
            {
                foreach (var detail in order.OrderDetails)
                {
                    var saldo = GetProductStockSaldo(detail.ProductInfo.Id);
                    if (saldo == null)
                        continue;

                    var diff = detail.Amount - detail.ReservedAmount;

                    if (saldo.Amount >= diff && detail.ReservedAmount != null)
                    {
                        saldo.Amount -= diff.Value;
                        detail.ReservedAmount += diff.Value;
                        productStocks.Add(saldo);
                    }
                    else if (detail.Amount > saldo.Amount && saldo.Amount != 0)
                    {
                        detail.ReservedAmount += saldo.Amount;
                        saldo.Amount = 0;
                    }
                }
            }
            UpdateOrder(orders);
            UpdateStock(productStocks);
            return productStocks;
        }

        public ProductStock GetProductStockSaldo(int productId)
        {
            return db.ProductStocks.SingleOrDefault(x => x.ProductInfoId == productId);
        }

        public void UpdateStock(List<ProductStock> productStocks)
        {
            foreach (var stock in productStocks)
            {
                db.ProductStocks.Attach(stock);
                var entry = db.Entry(stock);
                entry.State = EntityState.Modified;
            }
            db.SaveChanges();
        }

        public void UpdateStock(ProductStock productStocks)
        {
                db.ProductStocks.Attach(productStocks);
                var entry = db.Entry(productStocks);
                entry.State = EntityState.Modified;
                db.SaveChanges();
        }


        public void IsOrderComplete(List<Order> orders)
        {
            foreach (var order in orders)
            {
                order.OrderComplete = order.OrderDetails.All(x => x.Amount == x.ReservedAmount);
                UpdateOrder(order);
            }
        }

        public void ReturnProductToStock(int id)
        {
            var detail = db.OrderDetails.FirstOrDefault(od => od.Id == id);
            if (detail.Amount < detail.ReservedAmount.Value)
            {
                var diff = detail.ReservedAmount - detail.Amount;
                var saldo = GetProductStockSaldo(detail.ProductInfoId);
                saldo.Amount += diff.Value;
                detail.ReservedAmount = detail.Amount;
                UpdateStock(saldo);
                UpdateOrderdetail(detail);
            }
        }

        public void UpdateOrderdetail(OrderDetails orderDetails)
        {
            db.OrderDetails.Attach(orderDetails);
            var entry = db.Entry(orderDetails);
            entry.State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}