using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Trackyt.Core.Services;

namespace Trackyt.Core.Tests.Tests.Services
{
    [TestFixture]
    public class HashServiceTests
    {
        [Test]
        public void Smoke()
        {
            // arrange
            var service = new HashService();

            // act/assert
            Assert.That(service, Is.Not.Null);
        }

        [Test]
        public void CreateHash()
        {
            // arrange
            var service = new HashService();

            // act
            var hash = service.CreateMD5Hash("password");

            // assert
            Assert.That(hash, Is.Not.Empty);
        }

        [Test]
        public void ValidateHash()
        {
            // arrange
            var service = new HashService();

            // act
            var hash = service.CreateMD5Hash("password");
            var result = service.ValidateMD5Hash("password", hash);

            // assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void ValidateHash_False()
        {
            // arrange
            var service = new HashService();

            // act
            var hash = service.CreateMD5Hash("password");
            var result = service.ValidateMD5Hash("passworda", hash);

            // assert
            Assert.That(result, Is.False);
        }

        // here goes some paranoid tests of mine..

        [Test]
        public void ValidateHash_Length_1()
        {
            // arrange
            var service = new HashService();

            // act
            var hash = service.CreateMD5Hash("password");

            // assert
            Assert.That(hash.Length, Is.EqualTo(32));
        }

        [Test]
        public void ValidateHash_Length_2()
        {
            // arrange
            var service = new HashService();

            // act
            var hash = service.CreateMD5Hash("passwordaasa");

            // assert
            Assert.That(hash.Length, Is.EqualTo(32));
        }

        [Test]
        public void ValidateHash_Length_3()
        {
            // arrange
            var service = new HashService();

            // act
            var hash = service.CreateMD5Hash("password----");

            // assert
            Assert.That(hash.Length, Is.EqualTo(32));
        }


    }
}
