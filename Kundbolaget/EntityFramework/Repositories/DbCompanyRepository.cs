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
    public class DbCompanyRepository : IEntityRepository<Company>
    {
        private readonly StoreContext db = new StoreContext();

        public Company[] GetEntities()
        {
            return db.Companies.ToArray();
        }

        public Company GetEntity(int id)
        {
            
                return db.Companies.SingleOrDefault(c => c.Id == id);

            

            //using (var db = new StoreContext())
            //{
            //    return db.Companies
            //        .Include(c => c.Address)
            //        .Include(c => c.Country)
            //        .Include(c => c.ContactPerson)
            //        .Include(c => c.ParentCompany)
            //        .Include(c => c.DeliveryAddress)
            //        .SingleOrDefault(c => c.Id == id);
            //}
        }

        public void CreateEntity(Company newEntity)
        {
            db.Companies.Add(newEntity);
            db.SaveChanges();
        }

        public void DeleteEntity(int id)
        {
            throw new NotImplementedException();
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

    //public class DbAddressRepository : IEntityRepository<Address>
    //{
    //    private readonly StoreContext db = new StoreContext();

    //    public void Dispose()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Address[] GetEntities()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Address GetEntity(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void CreateEntity(Address newEntity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void DeleteEntity(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void UpdateEntity(Address updatedEntity)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //public class DbCountryRepository : IEntityRepository<Country>
    //{
    //    private readonly StoreContext db = new StoreContext();
    //    public void Dispose()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Country[] GetEntities()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Country GetEntity(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void CreateEntity(Country newEntity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void DeleteEntity(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void UpdateEntity(Country updatedEntity)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //public class DbContactPersonRepository : IEntityRepository<ContactPerson>
    //{
    //    private readonly StoreContext db = new StoreContext();

    //    public void Dispose()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public ContactPerson[] GetEntities()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public ContactPerson GetEntity(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void CreateEntity(ContactPerson newEntity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void DeleteEntity(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void UpdateEntity(ContactPerson updatedEntity)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //public class DbParentCompanyRepository : IEntityRepository<Company>
    //{
    //    private readonly StoreContext db = new StoreContext();

    //    public void Dispose()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Company[] GetEntities()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Company GetEntity(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void CreateEntity(Company newEntity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void DeleteEntity(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void UpdateEntity(Company updatedEntity)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    //public class DbDeliveryAddressRepository : IEntityRepository<Address>
    //{
    //    public void Dispose()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Address[] GetEntities()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public Address GetEntity(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void CreateEntity(Address newEntity)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void DeleteEntity(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void UpdateEntity(Address updatedEntity)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}