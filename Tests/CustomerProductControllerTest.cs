using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kundbolaget.Controllers;
using Kundbolaget.EntityFramework.Context;
using Kundbolaget.EntityFramework.Repositories;
using Kundbolaget.Models.EntityModels;
using Moq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    internal class CustomerProductControllerTest
    {
        private CustomerProductController _customerProductController;

        [SetUp]
        public void Initializer()
        {
            _customerProductController = new CustomerProductController();
        }

        [Test]
        public void View_Index()
        {
            var result = _customerProductController.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }
    }
}
