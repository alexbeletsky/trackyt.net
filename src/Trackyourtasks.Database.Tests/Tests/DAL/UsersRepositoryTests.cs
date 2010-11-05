using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Data.Linq;
using Trackyourtasks.Core.DAL;
using Trackyourtasks.Core.DAL.DataModel;
using Trackyourtasks.Core.DAL.Repositories.Impl;
using Trackyourtasks.Core.DAL.Extensions;
using Trackyourtasks.Core.Tests.Framework;

namespace Trackyourtasks.Core.DAL.Tests
{
    [TestFixture]
    public class UsersRepositoryTests
    {
        #region test data

        private void SubmitUsersToRepository(UsersRepository register, int usersCount, int tempUsers)
        {
            for (int users = 0; users < usersCount; users++)
            {
                register.SaveUser(new User { Email = users + "@a.com", Password = "pass", Temp = false });
            }

            for (int temp = 0; temp < tempUsers; temp++)
            {
                register.SaveUser(new User { Email = "temp" + temp + "@a.com", Password = "pass", Temp = true });
            }
        }

        #endregion

        [Test]
        public void Smoke()
        {
            var t = new UsersRepository();
            Assert.That(t, Is.Not.Null);
        }

        [Test]
        public void InsertUser()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //INIT
                var register = new UsersRepository(fixture.Setup.Context);

                //ACT
                var user = new User()
                {
                    Email = "email",
                    //SecretPhrase = "sec",
                    Password = "pass"
                };

                register.SaveUser(user);

                //POST
                var actual = register.Users.WithEmail("email");
                Assert.That(actual, Is.Not.Null);
            }
        }

        [Test]
        [ExpectedException(typeof(DuplicateKeyException))]
        public void InsertUserTwice()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //INIT
                var register = new UsersRepository(fixture.Setup.Context);

                //ACT / POST
                var user = new User()
                {
                    Email = "email",
                    //SecretPhrase = "sec",
                    Password = "pass"
                };

                register.SaveUser(user);

                var newUser = new User()
                {
                    Email = "email",
                    //SecretPhrase = "sec",
                    Password = "pass"
                };

                register.SaveUser(newUser);
            }
        }

        [Test]
        public void FindUserById()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //INIT
                var register = new UsersRepository(fixture.Setup.Context);

                var user = new User()
                {
                    Email = "email",
                    //SecretPhrase = "sec",
                    Password = "pass"
                };

                register.SaveUser(user);

                //ACT
                var foundUser = register.Users.WithId(user.Id);

                //POST
                Assert.That(foundUser, Is.Not.Null);
                Assert.That(foundUser.Id, Is.EqualTo(user.Id));
            }
        }

        [Test]
        public void UpdateUser()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //INIT
                var register = new UsersRepository(fixture.Setup.Context);

                var user = new User()
                {
                    Email = "email",
                    //SecretPhrase = "sec",
                    Password = "pass"
                };

                register.SaveUser(user);

                //ACT
                user.Email = "newsec";
                register.SaveUser(user);

                //POST
                var foundUser = register.Users.WithId(user.Id);
                Assert.That(foundUser, Is.Not.Null);
                Assert.That(foundUser.Email, Is.EqualTo("newsec"));
            }
        }

        [Test]
        public void DeleteUser()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //INIT
                var register = new UsersRepository(fixture.Setup.Context);

                var user = new User()
                {
                    Email = "email",
                    //SecretPhrase = "sec",
                    Password = "pass"
                };

                register.SaveUser(user);

                //ACT
                register.DeleteUser(user);

                //POST
                var foundUser = register.Users.WithId(user.Id);
                Assert.That(foundUser, Is.Null);
            }
        }

        [Test]
        public void WithTempExtension()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //arrange
                var register = new UsersRepository(fixture.Setup.Context);
                SubmitUsersToRepository(register, 5, 1);

                //act
                var usersCount = register.Users.WithTemp(false).Count();
                var tempCount = register.Users.WithTemp(true).Count();

                //post
                Assert.That(usersCount, Is.EqualTo(6 + 1)); // + 1, because 1 user added in DbSetup
                Assert.That(tempCount, Is.EqualTo(1));
            }
        }
    }
}
