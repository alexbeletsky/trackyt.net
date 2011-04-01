using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Trackyt.Core.DAL.Repositories.Impl;
using Trackyt.Core.Services;
using Trackyt.Core.Tests.Framework;
using Trackyt.Core.Services.Impl;

namespace Trackyt.Database.Tests.Tests.Services
{
    [TestFixture]
    public class NotificationServiceTests
    {
        // Note: This is integration test and ignored while usual unit test run
        // Only for manual testing
        [Test]
        [Ignore]
        public void NotifyUserOnRegistration_Integration_EmailSent()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                // assert
                var credentialsRepository = new CredentialsRepository(fixture.Setup.Context);
                var credentialService = new CredentialsService(credentialsRepository);
                var emailService = new EmailService(credentialService);
                var notificationService = new NotificationService(emailService);

                // act / assert
                notificationService.NotifyUserOnRegistration("alexander.beletsky@gmail.com", "password");
            }
        }
    }
}
