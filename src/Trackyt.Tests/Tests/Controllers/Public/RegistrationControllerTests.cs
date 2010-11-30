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

namespace Trackyt.Core.Tests.Controllers.Public
{
    [TestFixture]
    public class RegistrationControllerTests
    {
        [Test]
        public void Smoke()
        {
            //arrange
            var forms = new Mock<IFormsAuthentication>();
            var users = new Mock<IUsersRepository>();
            var controller = new RegistrationController(users.Object, forms.Object);
            //act/assert
            Assert.That(controller, Is.Not.Null);
        }

        [Test]
        public void Index()
        {
            //arrange
            var forms = new Mock<IFormsAuthentication>();
            var users = new Mock<IUsersRepository>();
            var controller = new RegistrationController(users.Object, forms.Object);
                    
            //act
            var result = controller.Index();

            //post
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Register_Post_Success_User_Added_To_Repository()
        {
            //arrange
            var forms = new Mock<IFormsAuthentication>();
            var usersRepository = new Mock<IUsersRepository>();
            var controller = new RegistrationController(usersRepository.Object, forms.Object);
            var model = new RegisterUserModel()
            {
                Email = "a@a.com",
                Password = "password",
                ConfirmPassword = "password"
            };

            var users = new List<User>();
            usersRepository.Setup(u => u.SaveUser(It.IsAny<User>())).Callback((User u) => users.Add(u)); 

            //act
            var result = controller.Register(model) as RedirectToRouteResult;

            //assert (repository)
            var user = users.Find(u => u.Email == "a@a.com");
            Assert.That(user, Is.Not.Null);
            Assert.That(user.Password, Is.EqualTo(model.Password));
        }

        [Test]
        public void Register_Post_Success_Redirected_To_Dashboard()
        {
            //arrange
            var forms = new Mock<IFormsAuthentication>();
            var usersRepository = new Mock<IUsersRepository>();
            var controller = new RegistrationController(usersRepository.Object, forms.Object);
            var model = new RegisterUserModel()
            {
                Email = "a@a.com",
                Password = "password",
                ConfirmPassword = "password"
            };

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
            var forms = new Mock<IFormsAuthentication>();
            var usersRepository = new Mock<IUsersRepository>();
            var controller = new RegistrationController(usersRepository.Object, forms.Object);
            var model = new RegisterUserModel()
            {
                Email = "a@a.com",
                Password = "password",
                ConfirmPassword = "password"
            };

            var users = new List<User>();
            usersRepository.Setup(u => u.SaveUser(It.IsAny<User>())).Callback((User u) => users.Add(u));
            usersRepository.Setup(u => u.Users).Returns(users.AsQueryable());

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
            var forms = new Mock<IFormsAuthentication>();
            var usersRepository = new Mock<IUsersRepository>();
            var controller = new RegistrationController(usersRepository.Object, forms.Object);
            var model = new RegisterUserModel()
            {
                Email = "a@a.com",
                Password = "password",
                ConfirmPassword = "password"
            };

            usersRepository.Setup(u => u.Users).Throws(new Exception());

            //act / post
            var result = controller.Register(model) as ViewResult;
        }

        [Test]
        public void QuickStart_Get_Success_Temp_User_Created()
        {
            //arrange
            var forms = new Mock<IFormsAuthentication>();
            var usersRepository = new Mock<IUsersRepository>();
            var controller = new RegistrationController(usersRepository.Object, forms.Object);

            var users = new List<User>();
            usersRepository.Setup(u => u.SaveUser(It.IsAny<User>())).Callback((User u) => users.Add(u));
            usersRepository.Setup(u => u.Users).Returns(users.AsQueryable());

            //act
            var resuts = controller.QuickStart() as RedirectResult;

            //post
            Assert.That(users.Count, Is.EqualTo(1), "new temporary user have to be added on quick registration");
            Assert.That(users.First().Temp, Is.True, "temp flag must be true for temporary users");
        }

        [Test]
        public void QuickStart_Get_Success_Temp_User_Name_Is_Unique()
        {
            //arrange
            var forms = new Mock<IFormsAuthentication>();
            var usersRepository = new Mock<IUsersRepository>();
            var controller = new RegistrationController(usersRepository.Object, forms.Object);

            var users = new List<User>();
            usersRepository.Setup(u => u.SaveUser(It.IsAny<User>())).Callback((User u) => users.Add(u));
            usersRepository.Setup(u => u.Users).Returns(users.AsQueryable());

            //act
            controller.QuickStart();
            controller.QuickStart();

            //post
            var email = users.First().Email;
            var unique = users.All(u => u.Email == email);

            Assert.That(unique, Is.False, "each registered users must have unique email");
        }

        [Test]
        public void QuickStart_Get_Success_Redirected_To_Dashboard()
        {
            //arrange
            var forms = new Mock<IFormsAuthentication>();
            var usersRepository = new Mock<IUsersRepository>();
            var controller = new RegistrationController(usersRepository.Object, forms.Object);

            //act
            var result = controller.QuickStart() as RedirectResult;

            //post
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Url, Is.EqualTo("~/User/Dashboard"));
        }

        [Test]
        public void QuickStart_Get_Success_User_Authenticated()
        {
            //arrange
            var forms = new Mock<IFormsAuthentication>();
            var usersRepository = new Mock<IUsersRepository>();
            var controller = new RegistrationController(usersRepository.Object, forms.Object);
            forms.Setup(f => f.GeneratePassword()).Returns("aaa111");

            //act
            var result = controller.QuickStart() as RedirectResult;

            //post
            forms.Verify(f => f.SetAuthCookie(It.IsAny<string>(), false));
        }

        [Test]
        public void Register_Post_Success_User_Authenticated()
        {
            //arrange
            var forms = new Mock<IFormsAuthentication>();
            var usersRepository = new Mock<IUsersRepository>();
            var controller = new RegistrationController(usersRepository.Object, forms.Object);
            var model = new RegisterUserModel()
            {
                Email = "a@a.com",
                Password = "password",
                ConfirmPassword = "password"
            };

            //act
            var result = controller.Register(model) as RedirectResult;

            //assert 
            forms.Verify(f => f.SetAuthCookie(It.IsAny<string>(), false));
        }

    }
}
