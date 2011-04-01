using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Trackyt.Core.Services;
using Trackyt.Core.Tests.Framework;
using Trackyt.Core.DAL.Repositories.Impl;
using Trackyt.Core.DAL.DataModel;
using Trackyt.Core.Services.Impl;

namespace Trackyt.Database.Tests.Tests.Services
{
    [TestFixture]
    public class ApiServicesTests
    {
        [Test]
        public void Smoke()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                // arrange
                var userRepository = new UsersRepository(fixture.Setup.Context);
                var hashService = new HashService();

                // act / post
                var service = new ApiService(userRepository, hashService);
                Assert.That(service, Is.Not.Null);
            }
        }

        [Test]
        public void GetApiToken_TokenForUser_ReturnedToken()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                // arrange
                var userRepository = new UsersRepository(fixture.Setup.Context);
                var hashService = new HashService();

                var service = new ApiService(userRepository, hashService);

                var email = "apiservtest@test.com";
                var password = "111111";
                var passwordHash = hashService.CreateMD5Hash(password);
                var apiToken = hashService.CreateApiToken(email, password);
                
                userRepository.Save(
                    new User
                    {
                        Email = email,
                        PasswordHash = passwordHash,
                        ApiToken = apiToken,
                        Temp = false
                    }
                );

                // act
                var result = service.GetApiToken(email, password);

                // assert
                Assert.That(result, Is.EqualTo(apiToken));
            }
        }

        [Test]
        public void GetApiToken_WrongPassword_ReturnedTokenWillBeNull()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                // arrange
                var userRepository = new UsersRepository(fixture.Setup.Context);
                var hashService = new HashService();

                var service = new ApiService(userRepository, hashService);

                var email = "apiservtest@test.com";
                var password = "111111";
                var passwordHash = hashService.CreateMD5Hash(password);
                var apiToken = hashService.CreateApiToken(email, password);

                userRepository.Save(
                    new User
                    {
                        Email = email,
                        PasswordHash = passwordHash,
                        ApiToken = apiToken,
                        Temp = false
                    }
                );

                // act
                var result = service.GetApiToken(email, password + "xx");

                // assert
                Assert.That(result, Is.Null);
            }
        }
    }
}
