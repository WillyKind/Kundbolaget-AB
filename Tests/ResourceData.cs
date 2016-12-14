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
                Name = "Flaska",
                Id = 1
            },
            new Container
            {
                Name = "Burk",
                Id = 2
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
            },
            new ProductGroup
            {
                Id = 2,
                Category = Category,
                Name = "Lager",
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
                PurchasePrice = 50,
                TradingMargin = 15,
                Description = "Kalas ska det vara."
            },
            new ProductInfo
            {
                Id = 2,
                ProductGroup = ProductGroups[1],
                Name = "Sofiero",
                Abv = 8,
                PurchasePrice = 30,
                TradingMargin = 10,
                Container = Containers[1],
                Volume = Volumes[1],
                Description = "Sofieros fina goda öl."
            }
        };
    }
}