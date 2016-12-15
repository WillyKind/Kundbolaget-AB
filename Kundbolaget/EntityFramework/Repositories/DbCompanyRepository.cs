using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Management.Instrumentation;
using System.Web;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.Interfaces;
using Kundbolaget.Models.EntityModels;

namespace Kundbolaget.EntityFramework.Repositories
{
    public class DbCompanyRepository : IEntityRepository<Company>
    {
        private readonly StoreContext db = new StoreContext();

        public Company[] GetEntities()
        {
            return db.Companies.Where(a => a.IsRemoved ==false).Include(c => c.Country).ToArray();
        }

        public Company GetEntity(int id)
        {
            return db.Companies
                    .Include(c => c.Country)
                    .Include(c => c.ParentCompany)
                    .SingleOrDefault(c => c.Id == id);
        }

        public void CreateEntity(Company newEntity)
        {
            db.Companies.Add(newEntity);
            db.SaveChanges();
        }

        public void DeleteEntity(int id)
        {
            var company = db.Companies.SingleOrDefault(c => c.Id == id);
            if (company != null)
            {
                company.IsRemoved = true;
                db.SaveChanges();
            }
        }

        public void UpdateEntity(Company updatedEntity)
        {
            db.Companies.Attach(updatedEntity);
            var entry = db.Entry(updatedEntity);
            entry.State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }

    
}