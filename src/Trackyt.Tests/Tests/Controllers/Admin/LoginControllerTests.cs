using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Web.Areas.Admin.Controllers;
using System.Web.Mvc;
using Web.Areas.Admin.Models;
using Moq;
using Web.Infrastructure.Security;
using Trackyt.Core.Services;

namespace Trackyt.Core.Tests.Tests.Controllers.Admin
{
    [TestFixture]
    public class LoginControllerTests
    {
        [Test]
        public void Smoke()
        {
            //arrange
            var auth = new Mock<IAuthenticationService>();
            var controller = new LoginController(auth.Object);

            //act/assert
            Assert.That(controller, Is.Not.Null);
        }

        [Test]
        public void GetIndex()
        {
            //arrange
            var auth = new Mock<IAuthenticationService>();
            var controller = new LoginController(auth.Object);

            //act
            var result = controller.Index() as ViewResult;

            //post
            Assert.That(result, Is.Not.Null);
        }
    }
}
