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
    public class ApiServiceTests
    {
        [Test]
        public void Smoke()
        {
            // arrange
            var hash = new Mock<IHashService>();
            var repository = new Mock<IUsersRepository>();
            var service = new ApiService(repository.Object, hash.Object);

            // act / post
            Assert.That(service, Is.Not.Null);
        }

        [Test]
        public void Authenticate_Success()
        {
            // arrange
            var hash = new Mock<IHashService>();
            var repository = new Mock<IUsersRepository>();

            var users = new List<User> { new User { Id = 1, ApiToken = "12345" }, new User { Id = 2, ApiToken = "23211" } };
            repository.Setup(r => r.Users).Returns(users.AsQueryable());

            var service = new ApiService(repository.Object, hash.Object);

            // act
            var result = service.GetUserByApiToken("23211");

            // assert
            Assert.That(result.Id, Is.EqualTo(2));
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void Authenticate_Fail()
        {
            // arrange
            var hash = new Mock<IHashService>();
            var repository = new Mock<IUsersRepository>();

            var users = new List<User> { new User { Id = 1, ApiToken = "12345" }, new User { Id = 2, ApiToken = "23211" } };
            repository.Setup(r => r.Users).Returns(users.AsQueryable());

            var service = new ApiService(repository.Object, hash.Object);

            // act
            var result = service.GetUserByApiToken("12346");

            // assert
        }

        [Test]
        public void GetApiToken_Success()
        {
            // arrange
            var hash = new Mock<IHashService>();
            var repository = new Mock<IUsersRepository>();

            var users = new List<User> { 
                new User { 
                    Id = 1, 
                    Email = "a@a.com", 
                    PasswordHash = "passhash", 
                    ApiToken = "12345" }, 
                new User { 
                    Id = 2, 
                    Email = "b@a.com", 
                    PasswordHash = "passhash2", 
                    ApiToken = "23211" 
                } };
            
            repository.Setup(r => r.Users).Returns(users.AsQueryable());

            hash.Setup(h => h.ValidateMD5Hash("pass", "passhash2")).Returns(true);

            var service = new ApiService(repository.Object, hash.Object);

            // act
            var token = service.GetApiToken("b@a.com", "pass");

            // post
            Assert.That(token, Is.Not.Null);
            Assert.That(token, Is.EqualTo("23211"));
        }

        [Test]
        public void GetApiToken_Fail()
        {
            // arrange
            var hash = new Mock<IHashService>();
            var repository = new Mock<IUsersRepository>();

            var users = new List<User> { 
                new User { 
                    Id = 1, 
                    Email = "a@a.com", 
                    PasswordHash = "passhash", 
                    ApiToken = "12345" }, 
                new User { 
                    Id = 2, 
                    Email = "b@a.com", 
                    PasswordHash = "passhash2", 
                    ApiToken = "23211" 
                } };

            repository.Setup(r => r.Users).Returns(users.AsQueryable());

            hash.Setup(h => h.ValidateMD5Hash("pass", "passhash2")).Returns(true);

            var service = new ApiService(repository.Object, hash.Object);

            // act
            var token = service.GetApiToken("b@a.com", "pass_wrong");

            // post
            Assert.That(token, Is.Null);
        }

        [Test]
        public void GetApiToken_NoSuchUser_ReturnNull()
        {
            // arrange
            var hash = new Mock<IHashService>();
            var repository = new Mock<IUsersRepository>();

            var users = new List<User> { 
                new User { 
                    Id = 1, 
                    Email = "a@a.com", 
                    PasswordHash = "passhash", 
                    ApiToken = "12345" }, 
                new User { 
                    Id = 2, 
                    Email = "b@a.com", 
                    PasswordHash = "passhash2", 
                    ApiToken = "23211" 
                } };

            repository.Setup(r => r.Users).Returns(users.AsQueryable());

            hash.Setup(h => h.ValidateMD5Hash("pass", "passhash2")).Returns(true);

            var service = new ApiService(repository.Object, hash.Object);

            // act
            var token = service.GetApiToken("user_wrong", "pass_wrong");

            // post
            Assert.That(token, Is.Null);
        }

    }
}
