using Kundbolaget.EntityFramework.Context;
using Kundbolaget.Models.EntityModels;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Kundbolaget.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<StoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(StoreContext context)
        {
            var volumes = new[]
            {
                new Volume {Milliliter = 33},
                new Volume {Milliliter = 5},
                new Volume {Milliliter = 75},
                new Volume {Milliliter = 100}
            };
            var containers = new[]
            {
                new Container {Name = "Burk"},
                new Container {Name = "Flaska"},
                new Container {Name = "Box"}
            };

            var categories = new[]
            {
                new Category {Name = "Öl"},
                new Category {Name = "Sprit"},
                new Category {Name = "Vin"},
            };

            var productGroups = new[]
            {
                new ProductGroup {Name = "Vitt vin", Category = categories[2]},
                new ProductGroup {Name = "Rött vin", Category = categories[2]},
                new ProductGroup {Name = "Rosé vin", Category = categories[2]},
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
                new ProductGroup {Name = "Likör", Category = categories[1]},
                new ProductGroup {Name = "Punch", Category = categories[1]},
                new ProductGroup {Name = "Calvados", Category = categories[1]},
            };

            var countries = new[]
            {
                new Country {Name = "Sweden", CountryCode = "+46", Region = "EMEA"},
                new Country {Name = "Norway", CountryCode = "+47", Region = "EMEA"},
                new Country {Name = "Finland", CountryCode = "+358", Region = "EMEA"},
                new Country {Name = "Denmark", CountryCode = "+45", Region = "EMEA"},
            };

            var contactPersons = new[]
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

            var addresses = new[]
            {
                new Address {Street = "Besöksvägen", Number = "1A", ZipCode = "111 11"},
                new Address {Street = "Lagervägen", Number = "1A", ZipCode = "000 00"},
                new Address {Street = "Leveransvägen", Number = "2B", ZipCode = "111 12"},
                new Address {Street = "Glimmervägen", Number = "1A", ZipCode = "111 11"},
                new Address {Street = "Leveransvägen", Number = "1A", ZipCode = "111 11"},
                new Address {Street = "Stadsvägen", Number = "1C", ZipCode = "112 11"}
            };


            var warehouse = new Warehouse
            {
                Name = "Kundbolagets Lager",
                AmmountOfStorageSpace = 500,
                ContactPerson = contactPersons.First(cp => cp.FirstName == "Viktor"),
                Email = "lager@kundbolaget.se",
                PhoneNumber = "+46899 00 00",
                Address = addresses.First(a => a.Street == "Lagervägen")
            };

            var wareHouse2 = new Warehouse
            {
                Name = "Kundbolagets mindre lager",
                AmmountOfStorageSpace = 200,
                ContactPerson = contactPersons.First(cp => cp.FirstName == "Willy"),
                Email = "mindreLager@kundbolaget.se",
                PhoneNumber = "+46899 00 01",
                Address = addresses.First(a => a.Street == "Stadsvägen")
            };

            var icaGruppen = new Company
            {
                Address = addresses.First(a => a.Street == "Besöksvägen"),
                ContactPerson = contactPersons.First(cp => cp.FirstName == "Johan"),
                Country = countries.First(c => c.Name == "Sweden"),
                DeliveryAddress = addresses.First(a => a.Street == "Besöksvägen"),
                Email = "icagruppen@ica.com",
                Name = "IcaGruppen",
                PhoneNumber = "+56899 22 22"
            };

            var anyIca = new Company
            {
                Address = addresses.First(a => a.Street == "Glimmervägen"),
                ContactPerson = contactPersons.First(cp => cp.FirstName == "Willy"),
                Country = countries.First(c => c.Name == "Sweden"),
                DeliveryAddress = addresses.First(a => a.Street == "Leveransvägen" && a.Number == "2B"),
                Email = "Icanågonstans@ica.com",
                PhoneNumber = "+46899 11 11",
                ParentCompany = icaGruppen,
                Name = "Ica någonstans"
            };

            var companies = new[]
            {
                icaGruppen, anyIca
            };

            var productInfoes = new[]
            {
                new ProductInfo
                {
                    Name = "Norrlandsguld",
                    Abv = 5.3,
                    Container = containers.First(c => c.Name == "Burk"),
                    Volume = volumes[0],
                    Description = "En burk med öl...",
                    ProductGroup = productGroups.First(pg => pg.Name == "Lager"),
                    PurchasePrice = 5.3,
                    TradingMargin = 10,
                    Price = 250
                },
                new ProductInfo
                {
                    Name = "Norrlandsguld",
                    Abv = 5.3,
                    Container = containers.First(c => c.Name == "Burk"),
                    Volume = volumes[1],
                    Description = "En burk med öl...",
                    ProductGroup = productGroups.First(pg => pg.Name == "Lager"),
                    PurchasePrice = 8.3,
                    TradingMargin = 8,
                    Price = 325
                },
                new ProductInfo
                {
                    Name = "Koskenkorva",
                    Abv = 40,
                    Container = containers.First(c => c.Name == "Flaska"),
                    Volume = volumes[2],
                    Description = "En Flaska sprit...",
                    ProductGroup = productGroups.First(pg => pg.Name == "Vodka"),
                    PurchasePrice = 35,
                    TradingMargin = 50,
                    Price = 900
                },
            };

            var stock = new[]
            {
                new ProductStock
                {
                    Amount = 500,
                    ProductInfo = productInfoes.First(pi => pi.Name == "Norrlandsguld"),
                    Warehouse = warehouse
                },
                new ProductStock
                {
                    Amount = 200,
                    ProductInfo = productInfoes.First(pi => pi.Name == "Koskenkorva"),
                    Warehouse = warehouse
                }
            };

            var dummyOrder = new Order
            {
                Company = companies.First(c => c.Name == "Ica någonstans"),
                CreatedDate = DateTime.Now,
                WishedDeliveryDate = DateTime.Parse("2016-12-12"),
            };

            var orderDetails = new[]
            {
                new OrderDetails
                {
                    ProductInfo = productInfoes.First(pi => pi.Name == "Koskenkorva"),
                    Amount = 500,
                    Order = dummyOrder
                },
                new OrderDetails
                {
                    ProductInfo = productInfoes.First(pi => pi.Name == "Norrlandsguld"),
                    Amount = 500,
                    Order = dummyOrder
                }
            };
            dummyOrder.Price += orderDetails.Sum(p => p.ProductInfo.Price*p.Amount);


            context.Countries.AddOrUpdate(countries);
            context.ContactPersons.AddOrUpdate(contactPersons);
            context.Categories.AddOrUpdate(categories);
            context.ProductGroups.AddOrUpdate(productGroups);
            context.Containers.AddOrUpdate(containers);
            context.Warehouses.AddOrUpdate(warehouse, wareHouse2);
            context.Companies.AddOrUpdate(companies);
            context.ProductsInfoes.AddOrUpdate(productInfoes);
            context.ProductStocks.AddOrUpdate(stock);
            context.Addresses.AddOrUpdate(addresses);
            context.OrderDetails.AddOrUpdate(orderDetails);
            context.SaveChanges();
        }
    }
}