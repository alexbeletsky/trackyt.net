using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Trackyourtasks.Core.Tests.Mocks;
using Trackyourtasks.Core.BLL;

namespace Trackyourtasks.Core.Tests.BLL
{
    [TestFixture]
    public class RegistrationTests
    {
        [Test]
        public void Smoke()
        {
            var t = new Registration(new RegistrationViewMock(), new UsersRepositoryMock(false));
        }

        [Test]
        public void RegisterNewUser()
        {
            //INIT
            var view = new RegistrationViewMock();
            var data = new UsersRepositoryMock(false);
            var register = new Registration(view, data);

            //ACT 
            register.RegisterUser("email", "secret phrase", "password");

            //POST
            Assert.That(view.RegisterSuccess, Is.True);
        }

        [Test]
        public void RegisterUserWithSameEmail()
        {
            //INIT
            var view = new RegistrationViewMock();
            var data = new UsersRepositoryMock(false);
            var register = new Registration(view, data);

            //ACT 
            register.RegisterUser("email", "secret phrase", "password");
            register = new Registration(view, data);
            register.RegisterUser("email", "secret phrase", "password");

            //POST
            Assert.That(view.RegisterSuccess, Is.False);
            Assert.That(view.FailureMessage, Is.EqualTo("Sorry, but user with same e-mail is already registered on site."));
        }

        [Test]
        public void RegisterFailed()
        {
            //INIT
            bool failOnRegister = true;
            var view = new RegistrationViewMock();
            var data = new UsersRepositoryMock(failOnRegister);
            var register = new Registration(view, data);

            //ACT 
            register.RegisterUser("email", "secret phrase", "password");

            //POST
            Assert.That(view.RegisterSuccess, Is.False);
            Assert.That(view.FailureMessage, Is.EqualTo("Sorry, but unexpected exception happened during operation."));
        }

        [Test]
        public void NoEmail()
        {
            //INIT
            var view = new RegistrationViewMock();
            var data = new UsersRepositoryMock(false);
            var register = new Registration(view, data);

            //ACT 
            register.RegisterUser(null, "secret phrase", "password");

            //POST
            Assert.That(view.RegisterSuccess, Is.False);
        }

        [Test]
        public void NoSecretPhrase()
        {
            //INIT
            var view = new RegistrationViewMock();
            var data = new UsersRepositoryMock(false);
            var register = new Registration(view, data);

            //ACT 
            register.RegisterUser("email", null, "password");

            //POST
            Assert.That(view.RegisterSuccess, Is.False);
        }

        [Test]
        public void NoPassword()
        {
            //INIT
            var view = new RegistrationViewMock();
            var data = new UsersRepositoryMock(false);
            var register = new Registration(view, data);

            //ACT 
            register.RegisterUser("email", "secret phrase", null);

            //POST
            Assert.That(view.RegisterSuccess, Is.False);
        }

    }
}
