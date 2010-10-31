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

namespace Trackyourtasks.Core.Tests.Tests.Controllers.Admin
{
    [TestFixture]
    public class AdminLoginControllerTests
    {
        [Test]
        public void Smoke()
        {
            //arrange
            var auth = new Mock<IFormsAuthentication>();
            var controller = new AdminLoginController(auth.Object);

            //act/assert
            Assert.That(controller, Is.Not.Null);
        }

        [Test]
        public void GetIndex()
        {
            //arrange
            var auth = new Mock<IFormsAuthentication>();
            var controller = new AdminLoginController(auth.Object);

            //act
            var result = controller.Index() as ViewResult;

            //post
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetAdminLogin_Success()
        {
            //arrange
            var auth = new Mock<IFormsAuthentication>();            
            var controller = new AdminLoginController(auth.Object);
            var model = new AdminLogin
            {
                Password = "trk$adm9cls!22"
            };
            
            //act
            var result = controller.Login(model) as RedirectResult;

            //assert
            auth.Verify(a => a.SetAuthCookie("TrackyAdmin", false));
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Url, Is.EqualTo("~/Admin/Dashboard"));
        }

        [Test]
        public void GetAdminLogin_Fail()
        {
            //arrange
            var auth = new Mock<IFormsAuthentication>();
            var controller = new AdminLoginController(auth.Object);
            var model = new AdminLogin
            {
                Password = "wrong"
            };

            //act
            var result = controller.Login(model) as RedirectResult;

            //assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Url, Is.EqualTo("~/Admin/AdminLogin"));
        }


    }
}
