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
                new Category {Name = "Öl"},
                new Category {Name = "Spritdrycker"},
                new Category {Name = "Vin"},
            };

            var productGroups = new ProductGroup[]
            {
                new ProductGroup {Name = "Vitt vin", Category = categories[2]},
                new ProductGroup {Name = "Rött vin", Category = categories[2]},
                new ProductGroup {Name = "Mouserande vin", Category = categories[2]},
                new ProductGroup {Name = "IPA", Category = categories[0]},
                new ProductGroup {Name = "APA", Category = categories[0]},
                new ProductGroup {Name = "Stout", Category = categories[0]},
                new ProductGroup {Name = "Rom", Category = categories[1]},
                new ProductGroup {Name = "Vodka", Category = categories[1]}
            };

            var countries = new Country[]
            {
                new Country {Name = "Sweden", CountryCode = "+46", Region = "EMEA"},
                new Country {Name = "Norway", CountryCode = "+47", Region = "EMEA"},
                new Country {Name = "Finland", CountryCode = "+358", Region = "EMEA"},
                new Country {Name = "Denmark", CountryCode = "+45", Region = "EMEA"},
            };

            var contactPersons = new ContactPerson[]
            {
                new ContactPerson
                {
                    FirstName = "Viktor",
                    LastName = "Gustafsson",
                    Email = "Viktor@randomcompany.com",
                    PhoneNumber = "+46899 99 99"
                },
                new ContactPerson
                {
                    FirstName = "Robert",
                    LastName = "Andersson",
                    Email = "Robert@randomcompany.com",
                    PhoneNumber = "+46899 88 88"
                },
                new ContactPerson
                {
                    FirstName = "Willy",
                    LastName = "Kind",
                    Email = "Willy@randomcompany.com",
                    PhoneNumber = "+46899 77 77"
                },
                new ContactPerson
                {
                    FirstName = "Michel",
                    LastName = "Miladinovic",
                    Email = "Johan@randomcompany.com",
                    PhoneNumber = "+46899 66 66"
                },
                new ContactPerson
                {
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