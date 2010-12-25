using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Data.Linq;
using Trackyt.Core.DAL;
using Trackyt.Core.DAL.DataModel;
using Trackyt.Core.DAL.Repositories.Impl;
using Trackyt.Core.DAL.Extensions;
using Trackyt.Core.Tests.Framework;

namespace Trackyt.Core.DAL.Tests
{
    [TestFixture]
    public class UsersRepositoryTests
    {
        #region test data

        private void SubmitUsersToRepository(UsersRepository register, int usersCount, int tempUsers)
        {
            for (int users = 0; users < usersCount; users++)
            {
                register.Save(new User { Email = users + "@a.com", Temp = false });
            }

            for (int temp = 0; temp < tempUsers; temp++)
            {
                register.Save(new User { Email = "temp" + temp + "@a.com", Temp = true });
            }
        }

        #endregion

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
                    //Password = "pass"
                };

                register.Save(user);

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
                    //Password = "pass"
                };

                register.Save(user);

                var newUser = new User()
                {
                    Email = "email",
                    //SecretPhrase = "sec",
                    //Password = "pass"
                };

                register.Save(newUser);
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
                    //Password = "pass"
                };

                register.Save(user);

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
                    //Password = "pass"
                };

                register.Save(user);

                //ACT
                user.Email = "newsec";
                register.Save(user);

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
                    //Password = "pass"
                };

                register.Save(user);

                //ACT
                register.Delete(user);

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
                Assert.That(usersCount, Is.EqualTo(6)); 
                Assert.That(tempCount, Is.EqualTo(1));
            }
        }
    }
}
