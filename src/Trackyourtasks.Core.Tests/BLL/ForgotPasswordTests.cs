using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Trackyourtasks.Core.BLL.Tests.Mocks;
using Trackyourtasks.Core.DAL.DataModel;

namespace Trackyourtasks.Core.BLL.Tests
{
    [TestFixture]
    public class ForgotPasswordTests
    {
        [Test]
        public void Smoke()
        {
            //INIT
            var data = new UsersRepositoryMock();

            //ACT/POST
            var c = new ForgotPassword(data);
        }

        [Test]
        public void RestorePassword_NoUser()
        {
            //INIT
            var data = new UsersRepositoryMock();
            var forgot = new ForgotPassword(data);

            //ACT
            var password = forgot.RestorePassword("no such user", "secret");

            //POST
            Assert.That(password, Is.Null, "It is not possible to return password for non-existing user.");
        }

        [Test]
        public void RestorePassword_WrongSecretPhrase()
        {
            //INIT
            var data = new UsersRepositoryMock();
            var forgot = new ForgotPassword(data);

            data.SaveUser(new User() { Email = "user@a.com", Password = "password", SecretPhrase = "correct secret" });

            //ACT
            var password = forgot.RestorePassword("user@a.com", "wrong secret");

            //POST
            Assert.That(password, Is.Null, "It is not possible to return password if wrong secret phrase used.");
        }

        [Test]
        public void RestorePassword_CorrectUserAndSecret()
        {
            //INIT
            var data = new UsersRepositoryMock();
            var forgot = new ForgotPassword(data);

            var expectedPassword = "password";
            data.SaveUser(new User() { Email = "user@a.com", Password = expectedPassword, SecretPhrase = "correct secret" });

            //ACT
            var password = forgot.RestorePassword("user@a.com", "correct secret");

            //POST
            Assert.That(password, Is.EqualTo(expectedPassword), "Wrong password value returned.");
        }
    }
}
