using System;
using System.Linq;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.Interfaces;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.EntityFramework.Repositories
{
    public class DbAddressRepository : IEntityRepository<Address>
    {
        private readonly StoreContext db = new StoreContext();

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

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}