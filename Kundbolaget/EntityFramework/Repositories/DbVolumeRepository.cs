using System;
using System.Linq;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.Interfaces;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.EntityFramework.Repositories
{
    public class DbVolumeRepository : IEntityRepository<Volume>
    {
        private readonly StoreContext db;

        public DbVolumeRepository(StoreContext fakeDb)
        {
            db = fakeDb;
        }

        public DbVolumeRepository()
        {
            db = new StoreContext();
        }

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