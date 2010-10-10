using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Web.Areas.Public.Controllers;
using Web.Areas.Public.Models;
using System.Web.Mvc;
using Moq;
using Web.Infrastructure.Security;

namespace Trackyourtasks.Core.Tests.Tests.Controllers.Public
{
    [TestFixture]
    public class LoginControllerTests
    {
        [Test]
        public void Smoke()
        {
            //arrange
            var auth = new Mock<IFormsAuthentication>().Object;
            var controller = new LoginController(new Mocks.UsersRepositoryMock(), auth);
            //act/post
            Assert.That(controller, Is.Not.Null);
        }

        [Test]
        public void Login_Index_View()
        {
            //arrange
            var auth = new Mock<IFormsAuthentication>().Object;
            var controller = new LoginController(new Mocks.UsersRepositoryMock(), auth);

            //act
            var result = controller.Index();

            //assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Login_Post_Success()
        {
            //arrange
            var auth = new Mock<IFormsAuthentication>();
            var repository = new Mocks.UsersRepositoryMock();
            var controller = new LoginController(repository, auth.Object);
            var model = new LoginModel() { Email = "a@a.com", Password = "xxx" };

            repository.SaveUser(new DAL.DataModel.User() { Email = "a@a.com", Password = "xxx" });

            //act
            var result = controller.Login(model, null) as RedirectResult;
            
            //assert
            auth.Verify(a => a.SetAuthCookie("a@a.com", false));
            Assert.That(result.Url, Is.EqualTo("/Tracky/Dashboard"));
        }

        [Test]
        public void Login_Post_Success_With_ReturnUrl()
        {
            //arrange
            var auth = new Mock<IFormsAuthentication>();
            var repository = new Mocks.UsersRepositoryMock();
            var controller = new LoginController(repository, auth.Object);
            var model = new LoginModel() { Email = "a@a.com", Password = "xxx" };

            repository.SaveUser(new DAL.DataModel.User() { Email = "a@a.com", Password = "xxx" });

            //act
            var result = controller.Login(model, "somewhere") as RedirectResult;

            //assert
            auth.Verify(a => a.SetAuthCookie("a@a.com", false));
            Assert.That(result.Url, Is.EqualTo("somewhere"));
        }

        [Test]
        public void Login_Post_Fail()
        {
            //arrange
            var auth = new Mock<IFormsAuthentication>();
            var repository = new Mocks.UsersRepositoryMock();
            var controller = new LoginController(repository, auth.Object);
            var model = new LoginModel() { Email = "a@a.com", Password = "xxx" };

            //act
            var result = controller.Login(model, "somewhere") as ViewResult;

            //assert
            Assert.That(controller.ModelState[""].Errors[0].ErrorMessage, Is.EqualTo("The user name or password provided is incorrect."));
        }

        [Test]
        public void Login_Post_Fail_WrongPassword()
        {
            //arrange
            var auth = new Mock<IFormsAuthentication>();
            var repository = new Mocks.UsersRepositoryMock();
            var controller = new LoginController(repository, auth.Object);
            var model = new LoginModel() { Email = "a@a.com", Password = "xxx" };

            repository.SaveUser(new DAL.DataModel.User() { Email = "a@a.com", Password = "yyy" });

            //act
            var result = controller.Login(model, "somewhere") as ViewResult;

            //assert
            Assert.That(controller.ModelState[""].Errors[0].ErrorMessage, Is.EqualTo("The user name or password provided is incorrect."));

        }
    }
}
