using System;
using System.Linq;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.Interfaces;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.EntityFramework.Repositories
{
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
}