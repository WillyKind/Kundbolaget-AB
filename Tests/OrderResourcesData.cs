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
                OrderDetails = new List<OrderDetails>
                {
                    new OrderDetails
                    {
                        ProductInfo = ResourceData.ProductInfoList[1],
                        Amount = 10,
                        Order = DummyOrder[0],
                        UnitPrice = ResourceData.ProductInfoList[1].Price,
                        TotalPrice = ResourceData.ProductInfoList[1].Price*10
                    },
                    new OrderDetails
                    {
                        ProductInfo = ResourceData.ProductInfoList[2],
                        Amount = 25,
                        UnitPrice = ResourceData.ProductInfoList[2].Price,
                        TotalPrice = ResourceData.ProductInfoList[2].Price*25,
                        Order = DummyOrder[0]
                    }
                }
            }
        };


        //static OrderDetails[] orderDetails = new[]
        //{
        //    new OrderDetails
        //    {
        //        ProductInfo = ResourceData.ProductInfoList[1],
        //        Amount = 10,
        //        Order = DummyOrder[0],
        //        UnitPrice = ResourceData.ProductInfoList[1].Price,
        //        TotalPrice = ResourceData.ProductInfoList[1].Price*10
        //    },
        //    new OrderDetails
        //    {
        //        ProductInfo = ResourceData.ProductInfoList[2],
        //        Amount = 25,
        //        UnitPrice = ResourceData.ProductInfoList[2].Price,
        //        TotalPrice = ResourceData.ProductInfoList[2].Price*25,
        //        Order = DummyOrder[0]
        //    }
        //};

        //    DummyOrder[0].Price += orderDetails.Sum(p => p.ProductInfo.Price*p.Amount);
    }
}