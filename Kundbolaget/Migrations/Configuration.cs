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
            var containers = new Container[]
            {
                new Container {Name = "Burk 0.33L", Volume = 0.33},
                new Container {Name = "Burk 0.5L", Volume = 0.5},
                new Container {Name = "Flaska 0.33L", Volume = 0.33},
                new Container {Name = "Flaska 0.5L", Volume = 0.5},
                new Container {Name = "Flaska 0.75L", Volume = 0.75},
                new Container {Name = "Flaska 1L", Volume = 1},
                new Container {Name = "Flaska 0.7L", Volume = 0.7},
                new Container {Name = "Flaska 0.35L", Volume = 0.35},
            };

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
                new ProductGroup {Name = "Lager", Category = categories[0]},
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

            var warehouse = new Warehouse
            {
                Name = "Kundbolagets Lager",
                AmmountOfStorageSpace = 500,
                ContactPerson = contactPersons[0],
                Email = "lager@kundbolaget.se",
                PhoneNumber = "+46899 00 00",
                Address = new Address {Street = "Lagervägen", Number = "1A", ZipCode = "000 00"}
            };


            var icaGruppen = new Company
            {
                Address = new Address {Street = "Besöksvägen", Number = "1A", ZipCode = "111 11"},
                ContactPerson = contactPersons[4],
                Country = countries[0],
                DeliveryAddress = new Address
                {
                    Street = "Leveransvägen",
                    Number = "2B",
                    ZipCode = "111 12"
                },
                Email = "icagruppen@ica.com",
                Name = "IcaGruppen",
                PhoneNumber = "+56899 22 22"
            };

            var anyIca = new Company
            {
                Address = new Address {Street = "Glimmervägen", Number = "1A", ZipCode = "111 11"},
                ContactPerson = contactPersons[3],
                Country = countries[0],
                DeliveryAddress = new Address
                {
                    Street = "Leveransvägen",
                    Number = "1A",
                    ZipCode = "111 11"
                },
                Email = "Icanågonstans@ica.com",
                PhoneNumber = "+46899 11 11",
                ParentCompany = icaGruppen,
                Name = "Ica någonstans"
            };

            var companies = new Company[]
            {
                icaGruppen, anyIca
            };

            var productInfoes = new ProductInfo[]
            {
                new ProductInfo
                {
                    Name = "Norrlandsguld 33cl",
                    Abv = 5.3,
                    Container = containers[0],
                    Description = "En burk med öl...",
                    ProductGroup = productGroups[4],
                    PurchasePrice = 5.3,
                    TradingMargin = 10,
                    Removed = false
                },
                new ProductInfo
                {
                    Name = "Norrlandsguld 50cl",
                    Abv = 5.3,
                    Container = containers[1],
                    Description = "En burk med öl...",
                    ProductGroup = productGroups[4],
                    PurchasePrice = 8.3,
                    TradingMargin = 8,
                    Removed = false
                }
            };

            context.Countries.AddOrUpdate(countries);
            context.ContactPersons.AddOrUpdate(contactPersons);
            context.Categories.AddOrUpdate(categories);
            context.ProductGroups.AddOrUpdate(productGroups);
            context.Containers.AddOrUpdate(containers);
            context.Warehouses.AddOrUpdate(warehouse);
            context.Companies.AddOrUpdate(companies);
            context.ProductsInfoes.AddOrUpdate(productInfoes);
            context.SaveChanges();
        }
    }
}