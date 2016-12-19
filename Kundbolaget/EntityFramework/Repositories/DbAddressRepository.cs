using System;
using System.Data.Entity;
using System.Linq;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.Interfaces;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.EntityFramework.Repositories
{
    public class DbAddressRepository : IEntityRepository<Address>
    {
        private readonly StoreContext _db;

        public DbAddressRepository(StoreContext mockContextObject) {
            _db = mockContextObject;

        }

        public DbAddressRepository() {
            _db = new StoreContext();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public Address[] GetEntities()
        {
            return _db.Addresses.Where(adress => adress.IsRemoved == false).ToArray();
        }

        public Address GetEntity(int id)
        {
            return _db.Addresses.SingleOrDefault(p => p.Id == id);
        }

        public void CreateEntity(Address newEntity)
        {
            _db.Addresses.Add(newEntity);
            _db.SaveChanges();
        }

        public void DeleteEntity(int id)
        {
            var product = _db.Addresses.SingleOrDefault(p => p.Id == id);
            if (product != null)
            {
                product.IsRemoved = true;
                _db.SaveChanges();
            }
        }

        public void UpdateEntity(Address updatedEntity)
        {
            _db.Addresses.Attach(updatedEntity);
            var entry = _db.Entry(updatedEntity);
            entry.State = EntityState.Modified;
            _db.SaveChanges();
        }
    }
}