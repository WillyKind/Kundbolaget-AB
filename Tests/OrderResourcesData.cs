using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kundbolaget.Models.EntityModels;

namespace Tests
{
    internal static class OrderResourcesData
    {
        public static List<Order> DummyOrder => new List<Order>
        {
            new Order
            {
                
                Company = new Company
                {
                    Address = new Address {Street = "Besöksvägen", Number = "1A", ZipCode = "111 11"}
                    ,
                    ContactPerson = new ContactPerson
                    {
                        FirstName = "Viktor",
                        LastName = "Gustafsson",
                        Email = "Viktor@randomcompany.com",
                        PhoneNumber = "+46899 99 99"
                    },
                    Country = new Country {Name = "Sweden", CountryCode = "+46", Region = "EMEA"},
                    DeliveryAddress = new Address {Street = "Besöksvägen", Number = "1A", ZipCode = "111 11"},
                    Email = "icavarberg@ica.com",
                    PhoneNumber = "+46899 11 11",
                    ParentCompany = new Company
                    {
                        Address = new Address {Street = "Besöksvägen", Number = "1A", ZipCode = "111 11"},
                        ContactPerson = new ContactPerson
                        {
                            FirstName = "Viktor",
                            LastName = "Gustafsson",
                            Email = "Viktor@randomcompany.com",
                            PhoneNumber = "+46899 99 99"
                        },
                        Country = new Country {Name = "Sweden", CountryCode = "+46", Region = "EMEA"},
                        DeliveryAddress = new Address {Street = "Besöksvägen", Number = "1A", ZipCode = "111 11"},
                        Email = "icagruppen@ica.com",
                        Name = "IcaGruppen",
                        PhoneNumber = "+56899 22 22"
                    },
                    Name = "Ica Vårberg"
                },
                CreatedDate = DateTime.Now,
                WishedDeliveryDate = DateTime.Parse("2016-12-12"),
                Price = 99,
                OrderDetails = new List<OrderDetails>
                {
                    new OrderDetails
                    {
                        Id = 1,
                        Amount = 10,
                        IsRemoved = false,
                        OrderId = 0,
                        ProductInfoId = 1,
                        UnitPrice = 50,
                        TotalPrice = 5000,
                        ReservedAmount = 10,
                        ProductInfo = new ProductInfo
                        {
                            Id = 1,
                            PalletDiscount = 10
                            
                        }
                    }
                },
                OrderDelivered = null,
                OrderPicked = null,
                OrderTransported = null
            }
        };
    }
}