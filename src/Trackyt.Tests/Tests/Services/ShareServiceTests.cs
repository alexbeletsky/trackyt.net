using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Trackyt.Core.Services.Impl;
using SharpTestsEx;
using Moq;
using Trackyt.Core.Services;
namespace Trackyt.Core.Tests.Tests.Services
{
    [TestFixture]
    public class ShareServiceTests
    {
        [Test]
        public void CreateShareLink_CreatedLinkAndToken()
        {
            // arrange
            var pathHelper = new Mock<IPathHelper>();
            var hashService = new HashService();
            var shareService = new ShareService(pathHelper.Object, hashService);

            pathHelper.SetReturnsDefault("/user/share");

            // act
            var link = shareService.CreateShareLink("alexander.beletsky@gmail.com");

            // assert
            link.Should().Contain("/user/share/alexander.beletsky@gmail.com?key=");
        }

        [Test]
        public void ValidateShareKey_CorrectKey()
        {
            var pathHelper = new Mock<IPathHelper>();
            var hashService = new HashService();
            var shareService = new ShareService(pathHelper.Object, hashService);

            pathHelper.SetReturnsDefault("/user/share");

            var key = "dd12113dcb92ffeb0d7afaf0261ffbfa";

            // act
            var result = shareService.ValidateShareKey("alexander.beletsky@gmail.com", key);

            // assert
            result.Should().Be.True();
        }

        [Test]
        public void ValidateShareKey_WrongKey()
        {
            var pathHelper = new Mock<IPathHelper>();
            var hashService = new HashService();
            var shareService = new ShareService(pathHelper.Object, hashService);

            pathHelper.SetReturnsDefault("/user/share");

            var key = "dd12113dcb92ffeb0d7afaf0261fAAAA";

            // act
            var result = shareService.ValidateShareKey("alexander.beletsky@gmail.com", key);

            // assert
            result.Should().Be.False();
        }

    }
}
