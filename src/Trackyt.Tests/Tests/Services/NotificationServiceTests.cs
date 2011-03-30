using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Trackyt.Core.Services;
using Moq;
using Trackyt.Core.DAL.Repositories.Impl;
using Trackyt.Core.Services.Impl;

namespace Trackyt.Core.Tests.Tests.Services
{
    [TestFixture]
    public class NotificationServiceTests
    {
        [Test]
        public void NotifyUserOnRegistration_SendEmailToUser_FromSupportAccount()
        {
            // arrange
            var emailService = new Mock<IEMailService>();
            var notificationService = new NotificationService(emailService.Object);

            // act 
            var usersEmail = "alexander.beletsky@gmail.com";
            notificationService.NotifyUserOnRegistration(usersEmail, "password");

            // assert
            emailService.Verify(e => e.SendEmail(It.IsAny<EmailMessage>(), "support"));
        }

        [Test]
        public void NotifyUserOnRegistration_SendEmailToUser_WithUsersEmail()
        {
            // arrange
            var emailService = new Mock<IEMailService>();
            var notificationService = new NotificationService(emailService.Object);

            var usersEmail = "alexander.beletsky@gmail.com";

            emailService.Setup(e => e.SendEmail(It.IsAny<EmailMessage>(), It.IsAny<string>()))
                .Callback((EmailMessage m, string c) => { Assert.That(m.To, Is.EqualTo(usersEmail), "Email TO is incorrect"); });

            // act 
            notificationService.NotifyUserOnRegistration(usersEmail, "password");

            // assert
            emailService.Verify(e => e.SendEmail(It.IsAny<EmailMessage>(), "support"), "Send mail has not been called");
        }

        [Test]
        public void NotifyUserOnRegistration_SendEmailToUser_BccSetToSupportEmail()
        {
            // arrange
            var emailService = new Mock<IEMailService>();
            var notificationService = new NotificationService(emailService.Object);

            var usersEmail = "alexander.beletsky@gmail.com";
            var supportEmail = "support@trackyt.net";
            emailService.Setup(e => e.SendEmail(It.IsAny<EmailMessage>(), It.IsAny<string>()))
                .Callback((EmailMessage m, string c) => { Assert.That(m.Bcc, Is.EqualTo(supportEmail), "Email BCC is incorrect"); });

            // act 
            notificationService.NotifyUserOnRegistration(usersEmail, "password");

            // assert
            emailService.Verify(e => e.SendEmail(It.IsAny<EmailMessage>(), "support"), "Send mail has not been called");
        }
    }
}
