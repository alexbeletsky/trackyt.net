using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Transactions;
using System.Data.Linq;
using Trackyourtasks.Core.DAL;
using Trackyourtasks.Core.DAL.DataModel;

namespace Trackyourtasks.Core.BLL.Tests
{
    [TestFixture]
    public class UsersRepositoryTests
    {
        TransactionScope _transaction;

        #region setup code

        [SetUp]
        public void Setup()
        {
            _transaction = new TransactionScope();
        }

        [TearDown]
        public void TearDown()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        #endregion

        [Test]
        public void Smoke()
        {
            var t = new UsersRepository();
        }

        [Test]
        public void InsertUser()
        {
            //INIT
            var register = new UsersRepository();

            //ACT
            var user = new User()
            {
                Email = "email",
                SecretPhrase = "sec",
                Password = "pass"
            };

            register.SaveUser(user);

            //POST
            var actual = register.FindUserByEmail("email");
            Assert.That(actual, Is.Not.Null);
        }

        [Test]
        [ExpectedException(typeof(DuplicateKeyException))]
        public void InsertUserTwice()
        {
            //INIT
            var register = new UsersRepository();

            //ACT / POST
            var user = new User()
            {
                Email = "email",
                SecretPhrase = "sec",
                Password = "pass"
            };

            register.SaveUser(user);

            var newUser = new User()
            {
                Email = "email",
                SecretPhrase = "sec",
                Password = "pass"
            };

            register.SaveUser(newUser);
        }

        [Test]
        public void FindUserById()
        {
            //INIT
            var register = new UsersRepository();

            var user = new User()
            {
                Email = "email",
                SecretPhrase = "sec",
                Password = "pass"
            };

            register.SaveUser(user);

            //ACT
            var foundUser = register.FindUserById(user.Id);

            //POST
            Assert.That(foundUser, Is.Not.Null);
            Assert.That(foundUser.Id, Is.EqualTo(user.Id));
        }

        [Test]
        public void UpdateUser()
        {
            //INIT
            var register = new UsersRepository();

            var user = new User()
            {
                Email = "email",
                SecretPhrase = "sec",
                Password = "pass"
            };

            register.SaveUser(user);

            //ACT
            user.SecretPhrase = "newsec";
            register.SaveUser(user);

            //POST
            var foundUser = register.FindUserById(user.Id);
            Assert.That(foundUser, Is.Not.Null);
            Assert.That(foundUser.SecretPhrase, Is.EqualTo("newsec"));
        }

        [Test]
        public void DeleteUser()
        {
            //INIT
            var register = new UsersRepository();

            var user = new User()
            {
                Email = "email",
                SecretPhrase = "sec",
                Password = "pass"
            };

            register.SaveUser(user);

            //ACT
            register.DeleteUser(user);

            //POST
            var foundUser = register.FindUserById(user.Id);
            Assert.That(foundUser, Is.Null);
        }
    }
}
