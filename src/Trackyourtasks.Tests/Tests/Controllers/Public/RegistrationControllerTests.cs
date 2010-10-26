using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Web.Areas.Public.Controllers;
using Web.Areas.Public.Models;
using System.Web.Mvc;
using System.Web.Routing;
using Trackyourtasks.Core.DAL.Extensions;
using Web.Infrastructure.Security;
using Moq;

namespace Trackyourtasks.Core.Tests.Controllers.Public
{
    //TODO: correct tests, to use Moq instead manually created Mocks
    [TestFixture]
    public class RegistrationControllerTests
    {
        [Test]
        public void Smoke()
        {
            //arrange
            var forms = new Mock<IFormsAuthentication>();
            var controller = new RegistrationController(new Mocks.UsersRepositoryMock(), forms.Object);
            //act/assert
            Assert.That(controller, Is.Not.Null);
        }

        [Test]
        public void Index()
        {
            //arrange
            var forms = new Mock<IFormsAuthentication>();
            var controller = new RegistrationController(new Mocks.UsersRepositoryMock(), forms.Object);
                    
            //act
            var result = controller.Index();

            //post
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Register_Get_ReturnsView()
        {
            //arrange
            var forms = new Mock<IFormsAuthentication>();
            var controller = new RegistrationController(new Mocks.UsersRepositoryMock(), forms.Object);

            //act
            var result = controller.Register();

            //assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Register_Post_Success_User_Added_To_Repository()
        {
            //arrange
            var forms = new Mock<IFormsAuthentication>();
            var repository = new Mocks.UsersRepositoryMock();
            var controller = new RegistrationController(repository, forms.Object);
            var model = new RegisterUserModel()
            {
                Email = "a@a.com",
                Password = "password",
                ConfirmPassword = "password"
            };

            //act
            var result = controller.Register(model) as RedirectToRouteResult;

            //assert (repository)
            var user = repository.Users.WithEmail("a@a.com");
            Assert.That(user, Is.Not.Null);
            Assert.That(user.Password, Is.EqualTo(model.Password));
        }

        [Test]
        public void Register_Post_Success_Redirected_To_Dashboard()
        {
            //arrange
            var forms = new Mock<IFormsAuthentication>();
            var repository = new Mocks.UsersRepositoryMock();
            var controller = new RegistrationController(repository, forms.Object);
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
            Assert.That(result.Url, Is.EqualTo("~/Tracky/Dashboard"));
        }

        [Test]
        public void Register_Post_Fail_Already_Registered()
        {
            //arrange
            var forms = new Mock<IFormsAuthentication>();
            var repository = new Mocks.UsersRepositoryMock();
            var controller = new RegistrationController(repository, forms.Object);
            var model = new RegisterUserModel()
            {
                Email = "a@a.com",
                Password = "password",
                ConfirmPassword = "password"
            };

            //act
            controller.Register(model);
            var result = controller.Register(model) as ViewResult;

            //assert
            Assert.That(model, Is.EqualTo(result.ViewData.Model));
            Assert.That(controller.ModelState[""].Errors[0].ErrorMessage, Is.EqualTo("Sorry, user with such email already exist. Please register with different email."));
        }

        [Test]
        public void Register_Post_Fail_Unknown_Reason()
        {
            //arrange
            var forms = new Mock<IFormsAuthentication>();
            var repository = new Mocks.UsersRepositoryMock(true);
            var controller = new RegistrationController(repository, forms.Object);
            var model = new RegisterUserModel()
            {
                Email = "a@a.com",
                Password = "password",
                ConfirmPassword = "password"
            };

            //act
            var result = controller.Register(model) as ViewResult;

            //post
            Assert.That(result, Is.Not.Null);
            Assert.That(result.ViewName, Is.EqualTo("Fail"));
            Assert.That(result.ViewData.Model, Is.TypeOf<Exception>()); 
        }

        [Test]
        public void QuickStart_Get_Success_Temp_User_Created()
        {
            //arrange
            var forms = new Mock<IFormsAuthentication>();
            var repository = new Mocks.UsersRepositoryMock();
            var controller = new RegistrationController(repository, forms.Object);

            //act
            var resuts = controller.QuickStart() as RedirectResult;

            //post
            var users = repository.Users;
            Assert.That(users.Count(), Is.EqualTo(1), "new temporary user have to be added on quick registration");
        }

        [Test]
        public void QuickStart_Get_Success_Temp_User_Name_Is_Unique()
        {
            //arrange
            var forms = new Mock<IFormsAuthentication>();
            var repository = new Mocks.UsersRepositoryMock();
            var controller = new RegistrationController(repository, forms.Object);

            //act
            controller.QuickStart();
            controller.QuickStart();

            //post
            var users = repository.Users;
            var email = users.First().Email;
            var unique = users.All(u => u.Email == email);

            Assert.That(unique, Is.False, "each registered users must have unique email");
        }

        [Test]
        public void QuickStart_Get_Success_Temp_Password_Is_Unique()
        {
            //arrange
            var forms = new TrackyFormsAuthentication();
            var repository = new Mocks.UsersRepositoryMock();
            var controller = new RegistrationController(repository, forms);

            //act
            controller.QuickStart();
            controller.QuickStart();

            //post
            var users = repository.Users;
            var pass = users.First().Password;
            var unique = users.All(u => u.Password == pass);

            Assert.That(unique, Is.False, "each registered users must have unique passwords");
        }

        [Test]
        public void QuickStart_Get_Success_Redirected_To_Dashboard()
        {
            //arrange
            var forms = new Mock<IFormsAuthentication>();
            var repository = new Mocks.UsersRepositoryMock();
            var controller = new RegistrationController(repository, forms.Object);

            //act
            var result = controller.QuickStart() as RedirectResult;

            //post
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Url, Is.EqualTo("~/Tracky/Dashboard"));
        }

        [Test]
        public void QuickStart_Get_Success_User_Authenticated()
        {
            //arrange
            var forms = new Mock<IFormsAuthentication>();
            var repository = new Mocks.UsersRepositoryMock();
            var controller = new RegistrationController(repository, forms.Object);
            forms.Setup(f => f.GeneratePassword()).Returns("aaa111");

            //act
            var result = controller.QuickStart() as RedirectResult;

            //post
            Assert.That(result, Is.Not.Null);
            forms.Verify(f => f.SetAuthCookie(It.IsAny<string>(), false));
        }

        [Test]
        public void Register_Post_Success_User_Authenticated()
        {
            //arrange
            var forms = new Mock<IFormsAuthentication>();
            var repository = new Mocks.UsersRepositoryMock();
            var controller = new RegistrationController(repository, forms.Object);
            var model = new RegisterUserModel()
            {
                Email = "a@a.com",
                Password = "password",
                ConfirmPassword = "password"
            };

            //act
            var result = controller.Register(model) as RedirectResult;

            //assert 
            Assert.That(result, Is.Not.Null);
            forms.Verify(f => f.SetAuthCookie(It.IsAny<string>(), false));
        }

    }
}
