using System.Net;
using System.Net.Mail;
using System;

namespace Trackyt.Core.Services
{
    public class EmailService : IEMailService
    {
        private ICredentialsService _credentials;

        public EmailService(ICredentialsService credentials)
        {
            _credentials = credentials;
        }

        public void SendEmail(EmailMessage message, string account)
        {
            MailMessage mm = new MailMessage(message.From, message.To, message.Subject, message.Message);
            mm.IsBodyHtml = message.IsHtml;

            NetworkCredential credentials = _credentials.GetCredentialsForAccount(account); //new NetworkCredential("support", "support123");
            if (credentials == null)
            {
                throw new Exception(String.Format(
                    "No credentials has been set for account: {0}. Please set up corresponding record in database", account
                    ));
            }

            var client = new SmtpClient();
            client.Credentials = credentials;
            client.Send(mm);
        }
    }
}
