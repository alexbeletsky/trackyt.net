using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Web.Controllers;
using Web.Models;
using System.Web.Mvc;
using System.Web.Routing;
using Trackyt.Core.DAL.Extensions;
using Web.Infrastructure.Security;
using Moq;
using Trackyt.Core.Security;
using Trackyt.Core.DAL.Repositories;
using Trackyt.Core.DAL.DataModel;
using Trackyt.Core.Services;

namespace Trackyt.Core.Tests.Controllers.Public
{
    [TestFixture]
    public class RegistrationControllerTests
    {
        [Test]
        public void Smoke()
        {
            //arrange
            var auth = new Mock<IAuthenticationService>();
            var controller = new RegistrationController(auth.Object);

            //act/assert
            Assert.That(controller, Is.Not.Null);
        }

        [Test]
        public void Index()
        {
            //arrange
            var auth = new Mock<IAuthenticationService>();
            var controller = new RegistrationController(auth.Object);

            //act
            var result = controller.Index();

            //post
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Register_Post_Success_Redirected_To_Dashboard()
        {
            //arrange
            var auth = new Mock<IAuthenticationService>();
            var controller = new RegistrationController(auth.Object);

            var model = new RegisterUserModel()
            {
                Email = "a@a.com",
                Password = "password",
                ConfirmPassword = "password"
            };

            auth.Setup(a => a.RegisterNewUser("a@a.com", "password", false)).Returns(true);
            auth.Setup(a => a.Authenticate("a@a.com", "password")).Returns(true);

            //act
            var result = controller.Register(model) as RedirectResult;

            //assert (result)
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Url, Is.EqualTo("~/User/Dashboard"));
        }

        [Test]
        public void Register_Post_Fail_Already_Registered()
        {
            //arrange
            var auth = new Mock<IAuthenticationService>();
            var controller = new RegistrationController(auth.Object);
            var model = new RegisterUserModel()
            {
                Email = "a@a.com",
                Password = "password",
                ConfirmPassword = "password"
            };

            auth.Setup(a => a.RegisterNewUser("a@a.com", "password", false)).Returns(false);
            //auth.Setup(a => a.Authenticate("a@a.com", "password")).Returns(true);

            //act
            controller.Register(model);
            var result = controller.Register(model) as ViewResult;

            //assert
            Assert.That(model, Is.EqualTo(result.ViewData.Model));
            Assert.That(controller.ModelState[""].Errors[0].ErrorMessage, Is.EqualTo("Sorry, user with such email already exist. Please register with different email."));
        }

        // yes, like this.. nothing to do, just throw an exception
        [Test]
        [ExpectedException(typeof(Exception))]
        public void Register_Post_Fail_Unknown_Reason()
        {
            //arrange
            var auth = new Mock<IAuthenticationService>();
            var controller = new RegistrationController(auth.Object);
            var model = new RegisterUserModel()
            {
                Email = "a@a.com",
                Password = "password",
                ConfirmPassword = "password"
            };

            auth.Setup(a => a.RegisterNewUser("a@a.com", "password", false)).Throws(new Exception());


            //act / post
            var result = controller.Register(model) as ViewResult;
        }

        [Test]
        public void QuickStart_Get_Success_Temp_User_Created()
        {
            //arrange
            var auth = new Mock<IAuthenticationService>();
            var controller = new RegistrationController(auth.Object);

            //act
            var resuts = controller.QuickStart() as RedirectResult;

            //post
            auth.Verify(a => a.RegisterNewUser(It.IsAny<string>(), It.IsAny<string>(), true));
        }

        [Test]
        public void QuickStart_Get_Success_Temp_User_Name_Is_Unique()
        {
            //arrange
            var auth = new Mock<IAuthenticationService>();
            var controller = new RegistrationController(auth.Object);

            var users = new List<dynamic>();
            auth.Setup(a => a.RegisterNewUser(It.IsAny<string>(), It.IsAny<string>(), true)).Callback(
                (string e, string p, bool t) =>
                { users.Add(new { Email = e, Password = p, Temp = t }); }
            );

            //act
            controller.QuickStart();
            controller.QuickStart();

            //post
            var groups = users.GroupBy(u => u.Email).Count();

            Assert.That(groups >= 2, Is.True, "each registered users must have unique email");
        }

        [Test]
        public void QuickStart_Get_Success_Redirected_To_Dashboard()
        {
            //arrange
            var auth = new Mock<IAuthenticationService>();
            var controller = new RegistrationController(auth.Object);

            //act
            var result = controller.QuickStart() as RedirectResult;

            //post
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Url, Is.EqualTo("~/User/Dashboard"));
        }

    }
}
