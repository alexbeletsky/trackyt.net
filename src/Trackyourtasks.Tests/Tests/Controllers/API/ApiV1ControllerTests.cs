using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Web.Controllers;

namespace Trackyourtasks.Core.Tests.Tests.Controllers.API
{
    [TestFixture]
    public class ApiV1ControllerTests
    {
        [Test]
        public void Smoke()
        {
            var api = new ApiV1Controller();
            Assert.That(api, Is.Not.Null);
        }

        [Test]
        public void GetAllTasks()
        {

        }
    }
}
