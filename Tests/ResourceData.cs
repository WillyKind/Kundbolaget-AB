using System.Collections.Generic;
using Kundbolaget.Models.EntityModels;

namespace Tests
{
    internal static class ResourceData
    {
        public static List<Volume> Volumes => new List<Volume>
        {
            new Volume
            {
                Milliliter = 330
            },
            new Volume
            {
                Milliliter = 500
            }
        };

        public static List<Container> Containers => new List<Container>
        {
            new Container
            {
                Name = "Flaska"
            },
            new Container
            {
                Name = "Burk"
            }
        };

        public static Category Category => new Category
        {
            Id = 1,
            Name = "Öl",
        };

        public static List<ProductGroup> ProductGroups => new List<ProductGroup>
        {
            new ProductGroup
            {
                Id = 1,
                Category = Category,
                Name = "Ale",
                CategoryId = Category.Id
            },
            new ProductGroup
            {
                Id = 2,
                Category = Category,
                Name = "Lager",
                CategoryId = Category.Id
            }
        };

        public static List<ProductInfo> ProductInfoList => new List<ProductInfo>
        {
            new ProductInfo
            {
                Id = 1,
                Container = Containers[0],
                Volume = Volumes[0],
                ProductGroup = ProductGroups[0],
                Abv = 7,
                Name = "Kalas Oscars finöl",
                Description = "Kalas ska det vara.",
                ProductGroupId = ProductGroups[0].Id,
            },
            new ProductInfo
            {
                Id = 2,
                ProductGroup = ProductGroups[1],
                Name = "Sofiero",
                Abv = 8,
                Container = Containers[1],
                Volume = Volumes[1],
                Description = "Sofieros fina goda öl.",
                ProductGroupId = ProductGroups[1].Id
            }
        };

        public static List<ProductStock> ProductStockList => new List<ProductStock>
        {
            new ProductStock
            {
                Id = 1,
                ProductInfo = ProductInfoList[0],
                ProductInfoId = 1,
                Amount = 500,
                Warehouse = WareHouseList[0],
                WarehouseId = 1
            },
            new ProductStock
            {
                Id = 2,
                ProductInfo = ProductInfoList[1],
                ProductInfoId = 2,
                Amount = 500,
                Warehouse = WareHouseList[0],
                WarehouseId = 1
            }
        };

        public static List<Warehouse> WareHouseList => new List<Warehouse>
        {
            new Warehouse
            {
               Id = 1,
               Name = "BarkarbyWarehouse",
               //Address = AdressList[0],
               AddressId = 1,
               AmmountOfStorageSpace = 400,
               Email = "barkarbywarehouse@gmail.com",
               PhoneNumber = "0701121212",
               //ContactPerson = ContactPersonList[0]
            }

        };

        public static List<Address> AdressList => new List<Address>
        {
            new Address()
            {
               Id = 1,
               Street = "Barkarbyvägen",
               Number = "44",
               ZipCode = "15569",
            },
            new Address
            {
               Id = 2,
               Street = "Fasanvägen",
               Number = "4",
               ZipCode = "13545",
            }
        };

        public static List<ContactPerson> ContactPersonList => new List<ContactPerson>
        {
            new ContactPerson()
            {
               Id = 1,
               Email = "johan.wanstrom@gmail.com",
               FirstName = "Johan",
               LastName = "Wänström",
               PhoneNumber = "0703334455"
            }
        };


    }
}