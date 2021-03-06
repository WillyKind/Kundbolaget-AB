﻿using System;
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
        private readonly StoreContext _db;

        public DbCompanyRepository(StoreContext mockContextObject)
        {
            _db = mockContextObject;
        }

        public DbCompanyRepository()
        {
            _db = new StoreContext();
        }

        public Company ValidateCompanyId(int id)
        {
            var company = _db.Companies.FirstOrDefault(c => c.Id == id && !c.IsRemoved);
            return company;
        }

        public Company[] GetChildCompanies(int id)
        {
            return _db.Companies.Where(c => c.ParentCompanyId == id && !c.IsRemoved).ToArray();
        }

        public Company[] GetParentCompanies()
        {
            return _db.Companies.Where(c => c.ParentCompany == null && !c.IsRemoved).ToArray();
        }

        public Company[] GetEntities()
        {
            return _db.Companies.Where(a => a.IsRemoved == false).Include(c => c.Country).ToArray();
        }

        public Company[] GetParentCompanies(int companyId)
            => _db.Companies.Where(company => company.Id != companyId && !company.IsRemoved).ToArray();

        public Company GetEntity(int id)
        {
            return _db.Companies
                .Include(c => c.Country)
                .Include(c => c.ParentCompany)
                .SingleOrDefault(c => c.Id == id);
        }

        public void CreateEntity(Company newEntity)
        {
            _db.Companies.Add(newEntity);
            _db.SaveChanges();
        }

        public void DeleteEntity(int id)
        {
            var company = _db.Companies.SingleOrDefault(c => c.Id == id);
            if (company != null)
            {
                company.IsRemoved = true;
                _db.SaveChanges();
            }
        }

        public void UpdateEntity(Company updatedEntity)
        {
            if (updatedEntity.AddressId == updatedEntity.DeliveryAddressId)
            {
                _db.Addresses.Add(updatedEntity.DeliveryAddress);
                _db.SetAdded(updatedEntity.DeliveryAddress);
            }
            else
            {
                _db.Addresses.Attach(updatedEntity.Address);
                _db.Addresses.Attach(updatedEntity.DeliveryAddress);
                _db.SetModified(updatedEntity.Address);
                _db.SetModified(updatedEntity.DeliveryAddress);
            }
            _db.Companies.Attach(updatedEntity);
            _db.SetModified(updatedEntity);
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}