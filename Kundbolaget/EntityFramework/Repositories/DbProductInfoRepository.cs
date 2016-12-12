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
            return db.ProductsInfoes.Where(x => x.IsRemoved == false).ToArray();
        }

        public ProductInfo GetEntity(int id)
        {
            using (var db = new StoreContext())
            {
                return db.ProductsInfoes
                    .Include(p => p.ProductGroup)
                    .Include(p => p.Container)
                    .Include(p => p.Volume)
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

    class DbVolumeRepository : IEntityRepository<Volume>
    {
        private readonly StoreContext db = new StoreContext();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Volume[] GetEntities()
        {
            return db.Volumes.ToArray();
        }

        public Volume GetEntity(int id)
        {
            throw new NotImplementedException();
        }

        public void CreateEntity(Volume newEntity)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateEntity(Volume updatedEntity)
        {
            throw new NotImplementedException();
        }
    }
    class DbAdressRepository : IEntityRepository<Address>
    {
        private readonly StoreContext db = new StoreContext();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Address[] GetEntities()
        {
            return db.Addresses.ToArray();
        }

        public Address GetEntity(int id)
        {
            throw new NotImplementedException();
        }

        public void CreateEntity(Address newEntity)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateEntity(Address updatedEntity)
        {
            throw new NotImplementedException();
        }
    }
}