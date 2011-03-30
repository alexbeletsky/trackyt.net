using System.Net;
using System.Net.Mail;
using System;

namespace Trackyt.Core.Services.Impl
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
            var fromAddress = new MailAddress(message.From);
            var toAddress = new MailAddress(message.To);

            var mailMessage = new MailMessage(fromAddress, toAddress);            
            mailMessage.Subject = message.Subject;

            if (message.Bcc != null)
            {
                mailMessage.Bcc.Add(new MailAddress(message.Bcc));
            }

            mailMessage.Body = message.Message;
            mailMessage.IsBodyHtml = message.IsHtml;

            var networkCredentials = _credentials.GetCredentialsForAccount(account);
            if (networkCredentials == null)
            {
                throw new Exception(String.Format(
                    "No credentials has been set for account: {0}. Please set up corresponding record in database", account
                    ));
            }

            var client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = networkCredentials;
            client.Host = "mail.trackyt.net";
            client.Port = 587;
            client.EnableSsl = false;
            client.Send(mailMessage);
        }
    }
}
