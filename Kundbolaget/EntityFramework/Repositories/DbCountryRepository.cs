using System;
using System.Linq;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.Interfaces;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.EntityFramework.Repositories
{
    public class DbCountryRepository : IEntityRepository<Country>
    {
        private readonly StoreContext db = new StoreContext();
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Country[] GetEntities()
        {
            return db.Countries.ToArray();
        }

        public Country GetEntity(int id)
        {
            throw new NotImplementedException();
        }

        public void CreateEntity(Country newEntity)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateEntity(Country updatedEntity)
        {
            throw new NotImplementedException();
        }
    }
}