using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Web.Areas.Public.Controllers;
using Web.Areas.Public.Models;

namespace Trackyourtasks.Core.Tests.Controllers.Public
{
    [TestFixture]
    public class RegistrationControllerTests
    {
        [Test]
        public void Smoke()
        {
            //arrange
            var controller = new RegistrationController();
            //act/assert
            Assert.That(controller, Is.Not.Null);
        }

        [Test]
        public void Index()
        {
            //arrange
            var controller = new RegistrationController();
                    
            //act
            var result = controller.Index();

            //post
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Register_Get_ReturnsView()
        {
            //arrange
            var controller = new RegistrationController();

            //act
            var result = controller.Register();

            //assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Register_Post_Success()
        {
            //arrange
            var controller = new RegistrationController();
            var model = new RegisterUserModel()
            {
                Email = "a@a.com",
                Password = "password",
                ConfirmPassword = "password"
            };

            //act
            var result = controller.Register(model);

            //assert
            Assert.That(result, Is.Not.Null);
        }

    }
}
