using System;
using System.Data.Entity;
using System.Linq;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.Interfaces;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.EntityFramework.Repositories
{
    class DbAdressRepository : IEntityRepository<Address>
    {
        private readonly StoreContext db = new StoreContext();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Address[] GetEntities()
        {
            return db.Addresses.Where(adress => adress.IsRemoved == false).ToArray();
        }

        public Address GetEntity(int id)
        {
            using (var db = new StoreContext())
            {
                return db.Addresses.SingleOrDefault(p => p.Id == id);
            }
        }

        public void CreateEntity(Address newEntity)
        {
            db.Addresses.Add(newEntity);
            db.SaveChanges();
        }

        public void DeleteEntity(int id)
        {
            var product = db.Addresses.SingleOrDefault(p => p.Id == id);
            if (product != null)
            {
                product.IsRemoved = true;
                db.SaveChanges();
            }
        }

        public void UpdateEntity(Address updatedEntity)
        {
            db.Addresses.Attach(updatedEntity);
            var entry = db.Entry(updatedEntity);
            entry.State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}