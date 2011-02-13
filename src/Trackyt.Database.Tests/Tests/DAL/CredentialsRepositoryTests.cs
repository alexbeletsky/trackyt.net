using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Trackyt.Core.Tests.Framework;
using Trackyt.Core.DAL.Repositories.Impl;

namespace Trackyt.Database.Tests.Tests.DAL
{
    [TestFixture]
    public class CredentialsRepositoryTests
    {
        [Test]
        public void Credentials_GetCredentials()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                // arrange 
                var repository = new CredentialsRepository(fixture.Setup.Context);

                // act
                var credentials = repository.Credentials;

                // assert
                Assert.That(credentials, Is.Not.Null);
                Assert.That(credentials.Count(), Is.EqualTo(0), "No credentials set in initial database.");
            }
        }
    }
}
