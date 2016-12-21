using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.Interfaces;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.EntityFramework.Repositories
{
    public class DbProductStockRepository 
    {
        private readonly StoreContext _storeContext;

        public DbProductStockRepository()
        {
            _storeContext = new StoreContext();
        }

        public DbProductStockRepository(StoreContext fakeDB)
        {
            _storeContext = fakeDB;
        }

        public void Dispose() => _storeContext.Dispose();

        public ProductStock[] GetEntities() => _storeContext.ProductStocks.ToArray();

        public ProductStock GetEntity(int id) => _storeContext.ProductStocks.FirstOrDefault(x => x.Id == id);

        public void CreateEntity(ProductStock newEntity)
        {
            _storeContext.ProductStocks.Add(newEntity);
            _storeContext.SaveChanges();
        }

        public void DeleteEntity(int id) => _storeContext.ProductStocks.Remove(_storeContext.ProductStocks.Find(id));

        public void UpdateEntity(ProductStock updatedEntity)
        {
            _storeContext.ProductStocks.Attach(updatedEntity);
            _storeContext.Entry(updatedEntity).State = EntityState.Modified;
            _storeContext.SaveChanges();
        }
    }
}