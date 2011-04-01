using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Trackyt.Core.Services;
using Moq;
using Trackyt.Core.DAL.Repositories;
using Trackyt.Core.DAL.DataModel;
using Trackyt.Core.Services.Impl;

namespace Trackyt.Core.Tests.Tests.Services
{
    [TestFixture]
    public class CredentialServicesTests
    {
        [Test]
        public void Smoke()
        {
            // arrange
            var repository = new Mock<ICredentialsRepository>();
            var service = new CredentialsService(repository.Object);

            // act/assert
            Assert.That(service, Is.Not.Null);
        }

        [Test]
        public void GetCredentials()
        {
            // arrange
            var repository = new Mock<ICredentialsRepository>();
            var service = new CredentialsService(repository.Object);

            var creds = new List<Credential> { new Credential { Account = "support", Email = "s@a.com", Password = "password" } };
            repository.Setup(r => r.Credentials).Returns(creds.AsQueryable());

            // act
            var result = service.GetCredentialsForAccount("support");

            // assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.UserName, Is.EqualTo("support"));
            Assert.That(result.Password, Is.EqualTo("password"));
        }

        [Test]
        public void GetCredentials_Returns_Null_If_Credentials_Not_Set()
        {
            // arrange
            var repository = new Mock<ICredentialsRepository>();
            var service = new CredentialsService(repository.Object);

            var creds = new List<Credential> { new Credential { Account = "support", Email = "s@a.com", Password = "password" } };
            repository.Setup(r => r.Credentials).Returns(creds.AsQueryable());

            // act
            var result = service.GetCredentialsForAccount("support-aaa");

            // assert
            Assert.That(result, Is.Null);
        }
    }
}
