using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.EntityFramework.Repositories
{
    public class DbOrderDetailsRpository
    {
        private StoreContext db;

        public DbOrderDetailsRpository()
        {
            db = new StoreContext();
        }
        public DbOrderDetailsRpository(StoreContext fakeContext)
        {
            db = fakeContext;
        }


        public OrderDetails GetEntity(int id)
        {
            return db.OrderDetails.FirstOrDefault(od => od.Id == id);

        }
        public void UpdateEntity(OrderDetails updatedEntity)
        {
            db.OrderDetails.Attach(updatedEntity);
            var entry = db.Entry(updatedEntity);
            entry.State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteEntity(int id)
        {
            var entityToRemove = db.OrderDetails.FirstOrDefault(od => od.Id == id);
            db.OrderDetails.Remove(entityToRemove);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}