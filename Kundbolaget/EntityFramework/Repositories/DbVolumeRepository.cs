using System;
using System.Linq;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.Interfaces;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.EntityFramework.Repositories
{
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
}