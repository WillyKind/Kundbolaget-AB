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
    public class DbProductInfoRepository : IEntityRepository<ProductInfo>
    {
        private readonly StoreContext db = new StoreContext();

        public ProductInfo[] GetEntities()
        {
            return db.ProductsInfoes.ToArray();
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
            db.ProductsInfoes.Remove(product);
            db.SaveChanges();
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

    class DbContainerRepository : IEntityRepository<Container>
    {
        private readonly StoreContext db = new StoreContext();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Container[] GetEntities()
        {
            return db.Containers.ToArray();
        }

        public Container GetEntity(int id)
        {
            throw new NotImplementedException();
        }

        public void CreateEntity(Container newEntity)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateEntity(Container updatedEntity)
        {
            throw new NotImplementedException();
        }
    }

    class DbProductGroupRepository : IEntityRepository<ProductGroup>
    {
        private readonly StoreContext db = new StoreContext();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ProductGroup[] GetEntities()
        {
            return db.ProductGroups.ToArray();
        }

        public ProductGroup GetEntity(int id)
        {
            throw new NotImplementedException();
        }

        public void CreateEntity(ProductGroup newEntity)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateEntity(ProductGroup updatedEntity)
        {
            throw new NotImplementedException();
        }
    }
}