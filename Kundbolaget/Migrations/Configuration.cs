using Kundbolaget.EntityFramework.Context;
using Kundbolaget.Models.EntityModels;
using Kundbolaget.Models;

namespace Kundbolaget.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(StoreContext context)
        {
            var categories = new Category[]
            {
                new Category {Id = 1, Name = "Öl"},
                new Category {Id = 2, Name = "Spritdrycker"},
                new Category {Id = 3, Name = "Vin"}
            };

            var productGroups = new ProductGroup[]
            {
                new ProductGroup {Id = 1, Name = "Vitt vin", CategoryId = 3},
                new ProductGroup {Id = 2, Name = "Rött vin", CategoryId = 3},
                new ProductGroup {Id = 3, Name = "Mouserande vin", CategoryId = 3},
                new ProductGroup {Id = 4, Name = "IPA", CategoryId = 1},
                new ProductGroup {Id = 5, Name = "APA", CategoryId = 1},
                new ProductGroup {Id = 6, Name = "Stout", CategoryId = 1},
                new ProductGroup {Id = 7, Name = "Rom", CategoryId = 2},
                new ProductGroup {Id = 8, Name = "Vodka", CategoryId = 2}
            };

            var countries = new Country[]
            {
                new Country {Id = 1, Name = "Sweden", CountryCode = "+46", Region = "EMEA"},
                new Country {Id = 2, Name = "Norway", CountryCode = "+47", Region = "EMEA"},
                new Country {Id = 3, Name = "Finland", CountryCode = "+358", Region = "EMEA"},
                new Country {Id = 4, Name = "Denmark", CountryCode = "+45", Region = "EMEA"},
            };

            var contactPersons = new ContactPerson[]
            {
                new ContactPerson
                {
                    Id = 1,
                    FirstName = "Viktor",
                    LastName = "Gustafsson",
                    Email = "Viktor@randomcompany.com",
                    PhoneNumber = "+46899 99 99"
                },
                new ContactPerson
                {
                    Id = 2,
                    FirstName = "Robert",
                    LastName = "Andersson",
                    Email = "Robert@randomcompany.com",
                    PhoneNumber = "+46899 88 88"
                },
                new ContactPerson
                {
                    Id = 3,
                    FirstName = "Willy",
                    LastName = "Kind",
                    Email = "Willy@randomcompany.com",
                    PhoneNumber = "+46899 77 77"
                },
                new ContactPerson
                {
                    Id = 4,
                    FirstName = "Michel",
                    LastName = "Miladinovic",
                    Email = "Johan@randomcompany.com",
                    PhoneNumber = "+46899 66 66"
                },
                new ContactPerson
                {
                    Id = 5,
                    FirstName = "Johan",
                    LastName = "Wänström",
                    Email = "Johan@randomcompany.com",
                    PhoneNumber = "+46899 55 55"
                }
            };

            context.Countries.AddOrUpdate(countries);
            context.ContactPersons.AddOrUpdate(contactPersons);
            context.Categories.AddOrUpdate(categories);
            context.ProductGroups.AddOrUpdate(productGroups);
            context.SaveChanges();
        }
    }
}