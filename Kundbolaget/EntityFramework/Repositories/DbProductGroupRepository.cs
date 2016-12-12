using System;
using System.Linq;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.Interfaces;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.EntityFramework.Repositories
{
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