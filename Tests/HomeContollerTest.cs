using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kundbolaget.Controllers;
using Kundbolaget.EntityFramework.Repositories;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    internal class HomeContollerTest
    {
        private HomeController _homeController;

        [SetUp]
        public void Initializer()
        {
            _homeController = new HomeController();
        }

        [Test]
        public void View_Index()
        {
            var result = _homeController.Index() as ViewResult;
            Assert.AreEqual("Index", result.ViewName);
        }

        [Test]
        public void View_AdminManager()
        {
            var result = _homeController.AdminManager() as ViewResult;
            Assert.AreEqual("AdminManager", result.ViewName);
        }

        [Test]
        public void View_CustomerSite()
        {
            var result = _homeController.CustomerSite() as ViewResult;
            Assert.AreEqual("CustomerSite", result.ViewName);
        }
    }
}