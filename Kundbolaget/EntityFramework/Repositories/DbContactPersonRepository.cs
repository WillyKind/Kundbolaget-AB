using System;
using System.Linq;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.Interfaces;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.EntityFramework.Repositories
{
    public class DbContactPersonRepository : IEntityRepository<ContactPerson>
    {
        private readonly StoreContext db = new StoreContext();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ContactPerson[] GetEntities()
        {
            return db.ContactPersons.ToArray();
        }

        public ContactPerson GetEntity(int id)
        {
            throw new NotImplementedException();
        }

        public void CreateEntity(ContactPerson newEntity)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateEntity(ContactPerson updatedEntity)
        {
            throw new NotImplementedException();
        }
    }
}