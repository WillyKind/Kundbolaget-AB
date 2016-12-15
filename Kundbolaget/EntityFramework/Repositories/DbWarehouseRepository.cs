using System.Linq;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.Interfaces;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.EntityFramework.Repositories
{
    public class DbWarehouseRepository : IEntityRepository<Warehouse>

    {
        private readonly StoreContext _storeContext;

        public DbWarehouseRepository()
        {
            _storeContext = new StoreContext();
        }
        public DbWarehouseRepository(StoreContext fakeDb)
        {
            _storeContext = fakeDb;
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public Warehouse[] GetEntities() => _storeContext.Warehouses.ToArray();

        public Warehouse GetEntity(int id)
        {
            throw new System.NotImplementedException();
        }

        public void CreateEntity(Warehouse newEntity)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteEntity(int id)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateEntity(Warehouse updatedEntity)
        {
            throw new System.NotImplementedException();
        }
    }
}