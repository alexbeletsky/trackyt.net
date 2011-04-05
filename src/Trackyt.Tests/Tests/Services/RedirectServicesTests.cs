using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Trackyt.Core.Services;
using SharpTestsEx;
using Trackyt.Core.Services.Impl;
namespace Trackyt.Core.Tests.Tests.Services
{
    [TestFixture]
    public class RedirectServicesTests
    {
        [Test]
        public void RedirectToDashboard_Redirects()
        {
            // arrange
            var service = new RedirectService();

            // act
            var result = service.ToDashboard("hey@joe.com");

            // assert
            result.Url.Should().Be("~/user/hey@joe.com");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void RedirectToDashboard_NullEmail_ThrowsException()
        {
            // arrange
            var service = new RedirectService();

            // act / assert
            service.ToDashboard(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void RedirectToDashboard_EmptyEmail_ThrowsException()
        {
            // arrange
            var service = new RedirectService();

            // act / assert
            service.ToDashboard("");
        }

        [Test]
        public void ToRegistrationSuccess_Redirects()
        {
            // arrange
            var service = new RedirectService();

            // act
            var result = service.ToRegistrationSuccess();

            // assert
            result.Url.Should().Be("~/registration/success");
        }

        [Test]
        public void ToUrl_WhatEverIWant_Redirects()
        {
            // arrange
            var service = new RedirectService();

            // act
            var result = service.ToUrl("whateveriwant");

            // assert
            result.Url.Should().Be("whateveriwant");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ToUrl_NullUrl_ThrowsException()
        {
            // arrange
            var service = new RedirectService();

            // act / assert
            service.ToUrl(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ToUrl_EmptyUrl_ThrowsException()
        {
            // arrange
            var service = new RedirectService();

            // act / assert
            service.ToUrl("");
        }
    }
}
