using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using Trackyt.Core.DAL.Repositories;
using Web.Infrastructure.Security;
using Trackyt.Core.DAL.DataModel;
using Trackyt.Core.Security;
using Trackyt.Core.Services;
using Trackyt.Core.Services.Impl;

namespace Trackyt.Core.Tests.Tests.Services
{
    [TestFixture]
    public class AuthenticationServicesTests
    {
        [Test]
        public void Smoke()
        {
            // arrange
            var users = new Mock<IUsersRepository>();
            var forms = new Mock<IFormsAuthentication>();
            var hash = new HashService();
            var service = new AuthenticationService(users.Object, forms.Object, hash);

            // act / post
            Assert.That(service, Is.Not.Null);
        }

        [Test]
        public void SuccessAuthentication()
        {
            // arrange
            var users = new Mock<IUsersRepository>();
            var forms = new Mock<IFormsAuthentication>();
            var hash = new HashService();
            var service = new AuthenticationService(users.Object, forms.Object, hash);

            users.Setup(u => u.Users).Returns(new List<User> { new User { Email = "ok@a.com", PasswordHash = hash.CreateMD5Hash("111"), Id = 1, Temp = false } }.AsQueryable());

            // act
            var auth = service.Authenticate("ok@a.com", "111");

            // post
            Assert.That(auth, Is.True);
            forms.Verify(f => f.SetAuthCookie("ok@a.com", false));
        }

        [Test]
        public void FailedAuthentication()
        {
            // arrange
            var users = new Mock<IUsersRepository>();
            var forms = new Mock<IFormsAuthentication>();
            var hash = new HashService();
            var service = new AuthenticationService(users.Object, forms.Object, hash);

            users.Setup(u => u.Users).Returns(new List<User> { new User { Email = "ok@a.com", PasswordHash = hash.CreateMD5Hash("111"), Id = 1, Temp = false } }.AsQueryable());
            forms.Setup(f => f.SetAuthCookie("", false)).Throws(new Exception("SetAuthCookie must not be called for failed authentication"));
            
            // act
            var auth = service.Authenticate("fail@a.com", "111");

            // post
            Assert.That(auth, Is.False);
        }

        [Test]
        public void FailedAuthentication_WrongPassword()
        {
            // arrange
            var users = new Mock<IUsersRepository>();
            var forms = new Mock<IFormsAuthentication>();
            var hash = new HashService();
            var service = new AuthenticationService(users.Object, forms.Object, hash);

            users.Setup(u => u.Users).Returns(new List<User> { new User { Email = "ok@a.com", PasswordHash = hash.CreateMD5Hash("111"), Id = 1, Temp = false } }.AsQueryable());
            forms.Setup(f => f.SetAuthCookie("", false)).Throws(new Exception("SetAuthCookie must not be called for failed authentication"));

            // act
            var auth = service.Authenticate("ok@a.com", "1111");

            // post
            Assert.That(auth, Is.False);
        }

        [Test]
        public void GetUserId()
        {
            // arrange
            var users = new Mock<IUsersRepository>();
            var forms = new Mock<IFormsAuthentication>();
            var hash = new HashService();
            var service = new AuthenticationService(users.Object, forms.Object, hash);

            users.Setup(u => u.Users).Returns(new List<User> { new User { Email = "ok@a.com", PasswordHash = hash.CreateMD5Hash("111"), Id = 1, Temp = false } }.AsQueryable());

            // act
            var id = service.GetUserIdByEmail("ok@a.com");

            // assert
            Assert.That(id, Is.EqualTo(1));
        }

        [Test]
        public void GetUserId_WrongUser()
        {
            // arrange
            var users = new Mock<IUsersRepository>();
            var forms = new Mock<IFormsAuthentication>();
            var hash = new HashService();
            var service = new AuthenticationService(users.Object, forms.Object, hash);

            users.Setup(u => u.Users).Returns(new List<User> { new User { Email = "ok@a.com", PasswordHash = hash.CreateMD5Hash("111"), Id = 1, Temp = false } }.AsQueryable());

            // act
            var id = service.GetUserIdByEmail("notok@a.com");

            // assert
            Assert.That(id, Is.EqualTo(0));
        }

        [Test]
        public void CreateNewUser_User_Added_To_Database()
        {
            // arrange
            var usersRepository = new Mock<IUsersRepository>();
            var forms = new Mock<IFormsAuthentication>();
            var hash = new HashService();
            var service = new AuthenticationService(usersRepository.Object, forms.Object, hash);

            var users = new List<User>();
            usersRepository.Setup(u => u.Users).Returns(users.AsQueryable());
            usersRepository.Setup(u => u.Save(It.IsAny<User>())).Callback((User u) => users.Add(u));

            // act
            service.RegisterNewUser("test@trackyt.net", "mypass");

            // assert
            Assert.That(users.Count, Is.GreaterThan(0));
            Assert.That(users.Find((u) => u.Email == "test@trackyt.net"), Is.Not.Null);
        }

        [Test]
        public void CreateNewUser_User_Hash_Created()
        {
            // arrange
            var usersRepository = new Mock<IUsersRepository>();
            var forms = new Mock<IFormsAuthentication>();
            var hash = new HashService();
            var service = new AuthenticationService(usersRepository.Object, forms.Object, hash);

            var users = new List<User>();
            usersRepository.Setup(u => u.Users).Returns(users.AsQueryable());
            usersRepository.Setup(u => u.Save(It.IsAny<User>())).Callback((User u) => users.Add(u));

            // act
            service.RegisterNewUser("test@trackyt.net", "mypass");

            // assert
            Assert.That(users.Count, Is.GreaterThan(0));
            var found = users.Find((u) => u.Email == "test@trackyt.net");
            Assert.That(found.PasswordHash.Length, Is.EqualTo(32));
        }

        [Test]
        public void CreateNewUser_User_Immediately_Auhtenticated()
        {
            //arrange
            var usersRepository = new Mock<IUsersRepository>();
            var forms = new Mock<IFormsAuthentication>();
            var hash = new HashService();
            var service = new AuthenticationService(usersRepository.Object, forms.Object, hash);

            var users = new List<User>();
            usersRepository.Setup(u => u.Users).Returns(users.AsQueryable());
            usersRepository.Setup(u => u.Save(It.IsAny<User>())).Callback((User u) => users.Add(u));

            // act
            service.RegisterNewUser("test@trackyt.net", "mypass");

            // assert
            Assert.That(users.Count, Is.GreaterThan(0));
            forms.Verify(f => f.SetAuthCookie("test@trackyt.net", false));
        }

        [Test]
        public void CreateNewUser_Returns_False_If_User_Exists()
        {
            // arrange
            var usersRepository = new Mock<IUsersRepository>();
            var forms = new Mock<IFormsAuthentication>();
            var hash = new HashService();
            var service = new AuthenticationService(usersRepository.Object, forms.Object, hash);

            var users = new List<User>();
            usersRepository.Setup(u => u.Users).Returns(users.AsQueryable());
            usersRepository.Setup(u => u.Save(It.IsAny<User>())).Callback((User u) => users.Add(u));

            // act
            service.RegisterNewUser("test@trackyt.net", "mypass");
            var result = service.RegisterNewUser("test@trackyt.net", "mypass");

            // assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void CreateNewUser_ApiToken_Created()
        {
            // arrange
            var usersRepository = new Mock<IUsersRepository>();
            var forms = new Mock<IFormsAuthentication>();
            var hash = new HashService();
            var service = new AuthenticationService(usersRepository.Object, forms.Object, hash);

            var users = new List<User>();
            usersRepository.Setup(u => u.Users).Returns(users.AsQueryable());
            usersRepository.Setup(u => u.Save(It.IsAny<User>())).Callback((User u) => users.Add(u));

            // act
            service.RegisterNewUser("test@trackyt.net", "mypass");

            // assert
            Assert.That(users.Count, Is.GreaterThan(0));
            var found = users.Find((u) => u.Email == "test@trackyt.net");
            Assert.That(found.ApiToken.Length, Is.EqualTo(32));

        }
    }
}
