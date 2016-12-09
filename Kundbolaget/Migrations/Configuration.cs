using Kundbolaget.EntityFramework.Context;
using Kundbolaget.Models.EntityModels;
using System.Collections.Generic;
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
            var volumes = new Volume[]
            {
                new Volume {Milliliter = 33},
                new Volume {Milliliter = 5},
                new Volume {Milliliter = 75},
                new Volume {Milliliter = 100}
            };
            var containers = new Container[]
            {
                new Container {Name = "Burk"},
                new Container {Name = "Flaska"},
                new Container {Name = "Box"}
            };

            var categories = new Category[]
            {
                new Category {Name = "�l"},
                new Category {Name = "Sprit"},
                new Category {Name = "Vin"},
            };

            var productGroups = new ProductGroup[]
            {
                new ProductGroup {Name = "Vitt vin", Category = categories[2]},
                new ProductGroup {Name = "R�tt vin", Category = categories[2]},
                new ProductGroup {Name = "Ros� vin", Category = categories[2]},
                new ProductGroup {Name = "Mouserande vin", Category = categories[2]},
                new ProductGroup {Name = "Portvin", Category = categories[2]},
                new ProductGroup {Name = "IPA", Category = categories[0]},
                new ProductGroup {Name = "ALE", Category = categories[0]},
                new ProductGroup {Name = "Lager", Category = categories[0]},
                new ProductGroup {Name = "APA", Category = categories[0]},
                new ProductGroup {Name = "Stout", Category = categories[0]},
                new ProductGroup {Name = "Rom", Category = categories[1]},
                new ProductGroup {Name = "Vodka", Category = categories[1]},
                new ProductGroup {Name = "Whiskey", Category = categories[1]},
                new ProductGroup {Name = "Konjak", Category = categories[1]},
                new ProductGroup {Name = "Lik�r", Category = categories[1]},
                new ProductGroup {Name = "Punch", Category = categories[1]},
                new ProductGroup {Name = "Calvados", Category = categories[1]},
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
                    LastName = "W�nstr�m",
                    Email = "Johan@randomcompany.com",
                    PhoneNumber = "+46899 55 55"
                }
            };

            var addresses = new Address[]
            {
                new Address {Street = "Bes�ksv�gen", Number = "1A", ZipCode = "111 11"},
                new Address {Street = "Lagerv�gen", Number = "1A", ZipCode = "000 00"},
                new Address {Street = "Leveransv�gen", Number = "2B", ZipCode = "111 12"},
                new Address {Street = "Glimmerv�gen", Number = "1A", ZipCode = "111 11"},
                new Address {Street = "Leveransv�gen", Number = "1A", ZipCode = "111 11"}
            };


            var warehouse = new Warehouse
            {
                Name = "Kundbolagets Lager",
                AmmountOfStorageSpace = 500,
                ContactPerson = contactPersons.First(cp => cp.FirstName == "Viktor"),
                Email = "lager@kundbolaget.se",
                PhoneNumber = "+46899 00 00",
                Address = addresses.First(a => a.Street == "Lagerv�gen")
            };


            var icaGruppen = new Company
            {
                Address = addresses.First(a => a.Street == "Bes�ksv�gen"),
                ContactPerson = contactPersons.First(cp => cp.FirstName == "Johan"),
                Country = countries.First(c => c.Name == "Sweden"),
                DeliveryAddress = addresses.First(a => a.Street == "Bes�ksv�gen"),
                Email = "icagruppen@ica.com",
                Name = "IcaGruppen",
                PhoneNumber = "+56899 22 22"
            };

            var anyIca = new Company
            {
                Address = addresses.First(a => a.Street == "Glimmerv�gen"),
                ContactPerson = contactPersons.First(cp => cp.FirstName == "Willy"),
                Country = countries.First(c => c.Name == "Sweden"),
                DeliveryAddress = addresses.First(a => a.Street == "Leveransv�gen" && a.Number == "2B"),
                Email = "Ican�gonstans@ica.com",
                PhoneNumber = "+46899 11 11",
                ParentCompany = icaGruppen,
                Name = "Ica n�gonstans"
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
                    Container = containers.First(c => c.Name == "Burk"),
                    Volume = volumes.First(v => v.Milliliter == 0.33),
                    Description = "En burk med �l...",
                    ProductGroup = productGroups.First(pg => pg.Name == "Lager"),
                    PurchasePrice = 5.3,
                    TradingMargin = 10
                },
                new ProductInfo
                {
                    Name = "Norrlandsguld 50cl",
                    Abv = 5.3,
                    Container = containers.First(c => c.Name == "Burk"),
                    Volume = volumes.First(v => v.Milliliter == 0.55),
                    Description = "En burk med �l...",
                    ProductGroup = productGroups.First(pg => pg.Name == "Lager"),
                    PurchasePrice = 8.3,
                    TradingMargin = 8,
                },
                new ProductInfo
                {
                    Name = "Koskenkorva 0.7L",
                    Abv = 40,
                    Container = containers.First(c => c.Name == "Flaska"),
                    Volume = volumes.First(v => v.Milliliter == 0.7),
                    Description = "En Flaska sprit...",
                    ProductGroup = productGroups.First(pg => pg.Name == "Vodka"),
                    PurchasePrice = 35,
                    TradingMargin = 50
                },
            };

            var stock = new ProductStock[]
            {
                new ProductStock
                {
                    Amount = 500,
                    ProductInfo = productInfoes.First(pi => pi.Name == "Norrlandsguld 33cl"),
                    Warehouses = new List<Warehouse> {warehouse}
                },
                new ProductStock
                {
                    Amount = 200,
                    ProductInfo = productInfoes.First(pi => pi.Name == "Koskenkorva 0.7L"),
                    Warehouses = new List<Warehouse> {warehouse}
                }
            };

            var dummyOrder = new Order
            {
                Company = companies.First(c => c.Name == "Ica n�gonstans"),
                CreatedDate = DateTime.Now,
            };

            var orderDetails = new OrderDetails[]
            {
                new OrderDetails {ProductInfo = productInfoes.First(pi=>pi.Name=="Koskenkorva 0.7L"),
                    Amount = 500,
                    Order = dummyOrder},
                 new OrderDetails {ProductInfo = productInfoes.First(pi=>pi.Name=="Norrlandsguld 33cl"),
                    Amount = 500,
                    Order = dummyOrder}
            };

            context.Countries.AddOrUpdate(countries);
            context.ContactPersons.AddOrUpdate(contactPersons);
            context.Categories.AddOrUpdate(categories);
            context.ProductGroups.AddOrUpdate(productGroups);
            context.Containers.AddOrUpdate(containers);
            context.Warehouses.AddOrUpdate(warehouse);
            context.Companies.AddOrUpdate(companies);
            context.ProductsInfoes.AddOrUpdate(productInfoes);
            context.ProductStocks.AddOrUpdate(stock);
            context.Addresses.AddOrUpdate(addresses);
            context.OrderDetails.AddOrUpdate(orderDetails);
            context.SaveChanges();
        }
    }
}