using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kundbolaget.Controllers;
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
    }
}
