using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Web.Controllers;
using Web.Models;
using System.Web.Mvc;
using Moq;
using Web.Infrastructure.Security;
using Trackyt.Core.Services;
using SharpTestsEx;
using Trackyt.Core.Services.Impl;

namespace Trackyt.Core.Tests.Tests.Controllers.Public
{
    [TestFixture]
    public class LoginControllerTests
    {
        [Test]
        public void Smoke()
        {
            // arrange
            var service = new Mock<IAuthenticationService>();
            var redirect = new RedirectService();
            var controller = new LoginController(service.Object, redirect);

            //act/post
            Assert.That(controller, Is.Not.Null);
        }


        [Test]
        public void Login_Index_View()
        {
            // arrange
            var service = new Mock<IAuthenticationService>();
            var redirect = new RedirectService();
            var controller = new LoginController(service.Object, redirect);

            //act
            var result = controller.Index();

            //assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Login_Post_Success()
        {
            // arrange
            var service = new Mock<IAuthenticationService>();
            var redirect = new RedirectService();
            var controller = new LoginController(service.Object, redirect);
            var model = new LoginModel() { Email = "a@a.com", Password = "xxx" };

            service.Setup(s => s.Authenticate("a@a.com", "xxx")).Returns(true);
            
            //act
            var result = controller.Login(model, null) as RedirectResult;

            //assert
            result.Url.Should().Be("~/user/a@a.com");
        }

        [Test]
        public void Login_Post_Success_With_ReturnUrl()
        {
            // arrange
            var service = new Mock<IAuthenticationService>();
            var redirect = new RedirectService();
            var controller = new LoginController(service.Object, redirect);
            var model = new LoginModel() { Email = "a@a.com", Password = "xxx" };

            service.Setup(s => s.Authenticate("a@a.com", "xxx")).Returns(true);

            //act
            var result = controller.Login(model, "somewhere") as RedirectResult;

            //assert
            Assert.That(result.Url, Is.EqualTo("somewhere"));
        }

        [Test]
        public void Login_Post_Fail()
        {
            // arrange
            var service = new Mock<IAuthenticationService>();
            var redirect = new RedirectService();
            var controller = new LoginController(service.Object, redirect);
            var model = new LoginModel() { Email = "a@a.com", Password = "xxx" };

            service.Setup(s => s.Authenticate("a@a.com", "xxx")).Returns(false);

            //act
            var result = controller.Login(model, "somewhere") as ViewResult;

            //assert
            Assert.That(controller.ModelState[""].Errors[0].ErrorMessage, Is.EqualTo("The user name or password provided is incorrect."));
        }
    }
}
