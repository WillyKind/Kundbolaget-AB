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
                new Volume {Milliliter = 250},
                new Volume {Milliliter = 330},
                new Volume {Milliliter = 350},
                new Volume {Milliliter = 355},
                new Volume {Milliliter = 500},
                new Volume {Milliliter = 700},
                new Volume {Milliliter = 750},
                new Volume {Milliliter = 1000},
                new Volume {Milliliter = 3000},
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

            var icaVarberg = new Company
            {
                Address = addresses.First(a => a.Street == "Glimmervägen"),
                ContactPerson = contactPersons.First(cp => cp.FirstName == "Willy"),
                Country = countries.First(c => c.Name == "Sweden"),
                DeliveryAddress = addresses.First(a => a.Street == "Leveransvägen" && a.Number == "2B"),
                Email = "icavarberg@ica.com",
                PhoneNumber = "+46899 11 11",
                ParentCompany = icaGruppen,
                Name = "Ica Vårberg"
            };

            var coop = new Company
            {
                Address = addresses.First(a => a.Street == "Besöksvägen"),
                ContactPerson = contactPersons.First(cp => cp.FirstName == "Michel"),
                Country = countries.First(c => c.Name == "Sweden"),
                DeliveryAddress = addresses.First(a => a.Street == "Besöksvägen"),
                Email = "Coop@medmera.com",
                Name = "Coop",
                PhoneNumber = "+56899 22 11"
            };

            var coopHaggvik = new Company
            {
                Address = addresses.First(a => a.Street == "Stadsvägen"),
                ContactPerson = contactPersons.First(cp => cp.FirstName == "Robert"),
                Country = countries.First(c => c.Name == "Sweden"),
                DeliveryAddress = addresses.First(a => a.Street == "Stadsvägen" && a.Number == "1C"),
                Email = "Coophaggvik@coop.com",
                PhoneNumber = "+46899 11 33",
                ParentCompany = coop,
                Name = "Coop Häggvik"
            };
            var coopLiljeholmen = new Company
            {
                Address = addresses.First(a => a.Street == "Besöksvägen"),
                ContactPerson = contactPersons.First(cp => cp.FirstName == "Robert"),
                Country = countries.First(c => c.Name == "Sweden"),
                DeliveryAddress = addresses.First(a => a.Street == "Lagervägen" && a.Number == "1A"),
                Email = "coopliljeholmen@coop.com",
                PhoneNumber = "+46899 33 33",
                ParentCompany = coop,
                Name = "Coop Liljeholmen"
            };
            var icaLiljeholmen = new Company
            {
                Address = addresses.First(a => a.Street == "Besöksvägen"),
                ContactPerson = contactPersons.First(cp => cp.FirstName == "Willy"),
                Country = countries.First(c => c.Name == "Sweden"),
                DeliveryAddress = addresses.First(a => a.Street == "Lagervägen" && a.Number == "1A"),
                Email = "icaliljeholmen@ica.com",
                PhoneNumber = "+46899 11 54",
                ParentCompany = icaGruppen,
                Name = "Ica Liljeholmen"
            };

            var companies = new[]
            {
                icaGruppen, icaVarberg, coop, coopLiljeholmen,coopHaggvik, icaLiljeholmen
            };

            var productInfoes = new[]
            {
                new ProductInfo
                {
                    Name = "Norrlandsguld",
                    Abv = 5.3,
                    Container = containers.First(c => c.Name == "Burk"),
                    Volume = volumes.First(v => v.Milliliter == 330),
                    Description = "Öl",
                    ProductGroup = productGroups.First(pg => pg.Name == "Lager"),
                    Price = 250,
                    NewPrice = 1337,
                    PriceStart = DateTime.Now.AddMinutes(1),
                    PalletDiscount = 10
                },
                new ProductInfo
                {
                    Name = "Norrlandsguld",
                    Abv = 5.3,
                    Container = containers.First(c => c.Name == "Burk"),
                    Volume = volumes.First(v => v.Milliliter == 500),
                    Description = "Öl",
                    ProductGroup = productGroups.First(pg => pg.Name == "Lager"),
                    Price = 325,
                    PalletDiscount = 10
                },
                new ProductInfo
                {
                    Name = "Nääs APA",
                    Abv = 5.8,
                    Container = containers.First(c => c.Name == "Flaska"),
                    Volume = volumes.First(v => v.Milliliter == 330),
                    Description = "APA",
                    ProductGroup = productGroups.First(pg => pg.Name == "APA"),
                    Price = 325,
                    PalletDiscount = 10
                },
                new ProductInfo
                {
                    Name = "Lagunitas IPA",
                    Abv = 6.2,
                    Container = containers.First(c => c.Name == "Flaska"),
                    Volume = volumes.First(v => v.Milliliter == 350),
                    Description = "IPA",
                    ProductGroup = productGroups.First(pg => pg.Name == "IPA"),
                    Price = 400,
                    PalletDiscount = 10
                },
                new ProductInfo
                {
                    Name = "Koskenkorva",
                    Abv = 40,
                    Container = containers.First(c => c.Name == "Flaska"),
                    Volume = volumes.First(v => v.Milliliter == 700),
                    Description = "Vodka",
                    ProductGroup = productGroups.First(pg => pg.Name == "Vodka"),
                    Price = 900,
                    PalletDiscount = 10
                },
                new ProductInfo
                {
                    Name = "Absolut Vodka",
                    Abv = 40,
                    Container = containers.First(c => c.Name == "Flaska"),
                    Volume = volumes.First(v => v.Milliliter == 700),
                    Description = "Vodka",
                    ProductGroup = productGroups.First(pg => pg.Name == "Vodka"),
                    Price = 1200,
                    PalletDiscount = 10
                },
                new ProductInfo
                {
                    Name = "Smirnoff Vodka",
                    Abv = 40,
                    Container = containers.First(c => c.Name == "Flaska"),
                    Volume = volumes.First(v => v.Milliliter == 700),
                    Description = "Vodka",
                    ProductGroup = productGroups.First(pg => pg.Name == "Vodka"),
                    Price = 1250,
                    PalletDiscount = 10
                },
                new ProductInfo
                {
                    Name = "Dreissigacker",
                    Abv = 12,
                    Container = containers.First(c => c.Name == "Flaska"),
                    Volume = volumes.First(v => v.Milliliter == 700),
                    Description = "Riesling",
                    ProductGroup = productGroups.First(pg => pg.Name == "Vitt vin"),
                    Price = 1250,
                    PalletDiscount = 10
                },
                new ProductInfo
                {
                    Name = "Vino Nobile di Montepulciano",
                    Abv = 13.5,
                    Container = containers.First(c => c.Name == "Flaska"),
                    Volume = volumes.First(v => v.Milliliter == 700),
                    Description = "Sangiovese",
                    ProductGroup = productGroups.First(pg => pg.Name == "Rött vin"),
                    Price = 1250,
                    PalletDiscount = 10
                },
            };

            var stock = new[]
            {
                new ProductStock
                {
                    Amount = 500,
                    ProductInfo = productInfoes.First(pi => pi.Name == "Norrlandsguld" && pi.Volume.Milliliter == 330),
                    Warehouse = warehouse
                },
                new ProductStock
                {
                    Amount = 500,
                    ProductInfo = productInfoes.First(pi => pi.Name == "Norrlandsguld" && pi.Volume.Milliliter == 500),
                    Warehouse = warehouse
                },
                new ProductStock
                {
                    Amount = 200,
                    ProductInfo = productInfoes.First(pi => pi.Name == "Nääs APA" && pi.Volume.Milliliter == 330),
                    Warehouse = warehouse
                },
                new ProductStock
                {
                    Amount = 300,
                    ProductInfo = productInfoes.First(pi => pi.Name == "Lagunitas IPA" && pi.Volume.Milliliter == 350),
                    Warehouse = warehouse
                },
                new ProductStock
                {
                    Amount = 150,
                    ProductInfo = productInfoes.First(pi => pi.Name == "Koskenkorva" && pi.Volume.Milliliter == 700),
                    Warehouse = warehouse
                },
                new ProductStock
                {
                    Amount = 150,
                    ProductInfo = productInfoes.First(pi => pi.Name == "Absolut Vodka" && pi.Volume.Milliliter == 700),
                    Warehouse = warehouse
                },
                new ProductStock
                {
                    Amount = 150,
                    ProductInfo = productInfoes.First(pi => pi.Name == "Smirnoff Vodka" && pi.Volume.Milliliter == 700),
                    Warehouse = warehouse
                },
                new ProductStock
                {
                    Amount = 150,
                    ProductInfo = productInfoes.First(pi => pi.Name == "Dreissigacker" && pi.Volume.Milliliter == 700),
                    Warehouse = warehouse
                },
                new ProductStock
                {
                    Amount = 150,
                    ProductInfo = productInfoes.First(pi => pi.Name == "Vino Nobile di Montepulciano" && pi.Volume.Milliliter == 700),
                    Warehouse = warehouse
                },
            };

            //var dummyOrder = new Order
            //{
            //    Company = companies.First(c => c.Name == "Ica Vårberg"),
            //    CreatedDate = DateTime.Now,
            //    WishedDeliveryDate = DateTime.Parse("2016-12-12"),
            //    OrderComplete = false
            //};

            //var orderDetails = new[]
            //{
            //    new OrderDetails
            //    {
            //        ProductInfo = productInfoes.First(pi => pi.Name == "Koskenkorva"),
            //        Amount = 10,
            //        Order = dummyOrder,
            //        UnitPrice = productInfoes.First(pi => pi.Name == "Koskenkorva").Price,
            //        TotalPrice = productInfoes.First(pi => pi.Name == "Koskenkorva").Price*10,
            //        ReservedAmount = 0
            //    },
            //    new OrderDetails
            //    {
            //        ProductInfo = productInfoes.First(pi => pi.Name == "Norrlandsguld"),
            //        Amount = 25,
            //        UnitPrice = productInfoes.First(pi => pi.Name == "Norrlandsguld").Price,
            //        TotalPrice = productInfoes.First(pi => pi.Name == "Norrlandsguld").Price*25,
            //        Order = dummyOrder,
            //        ReservedAmount = 0
            //    }
            //};
            //dummyOrder.Price += orderDetails.Sum(p => p.ProductInfo.Price * p.Amount);


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
            //context.OrderDetails.AddOrUpdate(orderDetails);
            context.Volumes.AddOrUpdate(volumes);
            context.SaveChanges();
        }
    }
}