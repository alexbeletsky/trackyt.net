using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
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

            register.InsertUser(user);

            //POST
            var actual = register.FindUserByEmail("email");
            Assert.That(actual, Is.Not.Null);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
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

            register.InsertUser(user);
            register.InsertUser(user);
        }
    }
}
