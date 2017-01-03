using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.Interfaces;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.EntityFramework.Repositories
{
    public class DbProductInfoRepository
    {
        private readonly StoreContext db;

        public DbProductInfoRepository()
        {
            db = new StoreContext();
        }

        public DbProductInfoRepository(StoreContext fakeContext)
        {
            db = fakeContext;
        }

        public ProductInfo[] GetEntities()
        {
            return db.ProductsInfoes
                .Where(x => x.IsRemoved == false)
                .ToArray()
                .Select(UpdatePrice)
                .ToArray();
        }

        public ProductInfo GetEntity(int id)
        {
            return UpdatePrice(db.ProductsInfoes
                .Include(p => p.ProductGroup)
                .Include(p => p.Container)
                .SingleOrDefault(p => p.Id == id));
        }

        public void CreateEntity(ProductInfo newEntity)
        {
            db.ProductsInfoes.Add(newEntity);
            db.SaveChanges();
        }

        public ProductInfo UpdatePrice(ProductInfo product)
        {
            if (!product.NewPrice.HasValue || !product.PriceStart.HasValue || !(product.PriceStart <= DateTime.Now))
                return product;
            product.Price = product.NewPrice.Value;
            product.NewPrice = null;
            product.PriceStart = null;
            UpdateEntity(product);
            return product;
        }

        public void DeleteEntity(int id)
        {
            var product = db.ProductsInfoes.SingleOrDefault(p => p.Id == id);
            if (product != null)
            {
                product.IsRemoved = true;
                db.SaveChanges();
            }
        }

        public void UpdateEntity(ProductInfo updatedEntity)
        {
            db.ProductsInfoes.Attach(updatedEntity);
            var entry = db.Entry(updatedEntity);
            entry.State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}