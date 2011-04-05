using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyt.Core.DAL.DataModel;

namespace Trackyt.Core.Services.Impl
{
    public class NotificationService : INotificationService
    {
        private IEMailService _emailService;

        public NotificationService(IEMailService emailService)
        {
            _emailService = emailService;
        }

        public void NotifyUserOnRegistration(string usersEmail, string password)
        {
            var emailMessage = CreateEmailMessageForUser(usersEmail, password);
            _emailService.SendEmail(emailMessage, "support");
        }

        private static EmailMessage CreateEmailMessageForUser(string usersEmail, string password)
        {
            var message = String.Format(
                    "<p>Dear {0},</p>" +
                    "<p>" +
                        "Thank you very much for registration on <a href=\"http://trackyt.net\">Trackyt.net</a>." +
                        " You can now start with time tracking tasks very easy." +
                    "</p>" +
                    "<p>" +
                        "You password used during registration is: {1}" +
                    "<p>" +
                        "Keep this email, because we will not be able to remind you password if you lost it. In case of any questions, please just respond to this email." +
                    "</p>" +
                    "<p>" +
                        "Please follow our Twitter for news and updates: " +
                    "</p>" +
                    "<p>" +
                       "<a href=\"http://twitter.com/#!/trackytnet\">http://twitter.com/#!/trackytnet</a>" +
                    "</p>" +
                    "<p></p>" +
                    "<p><i>trackyt.net support team</i></p>" +
                    "<p>@trackytnet</p>"
                , usersEmail, password);

            var email = new EmailMessage
            {
                From = "support@trackyt.net",
                Bcc = "support@trackyt.net",
                To = usersEmail,
                Message = message,
                Subject = "Welcome to trackyt.net!",
                IsHtml = true
            };

            return email;
        }

    }
}
