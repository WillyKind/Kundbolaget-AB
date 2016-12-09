using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.Interfaces;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.EntityFramework.Repositories
{
    public class DbProductInfoRepository : IEntityRepository<ProductInfo>
    {
        private readonly StoreContext db = new StoreContext();

        public ProductInfo[] GetEntities()
        {
            return db.ProductsInfoes.Where(x => x.IsRemoved == false).ToArray();
        }

        public ProductInfo GetEntity(int id)
        {
            using (var db = new StoreContext())
            {
                return db.ProductsInfoes
                    .Include(p => p.ProductGroup)
                    .Include(p => p.Container)
                    .SingleOrDefault(p => p.Id == id);
            }
        }

        public void CreateEntity(ProductInfo newEntity)
        {
            db.ProductsInfoes.Add(newEntity);
            db.SaveChanges();
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

        public void Dispose()
        {
            db.Dispose();
        }
    }
}