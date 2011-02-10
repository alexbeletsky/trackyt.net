using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trackyt.Core.Services
{
    public class EmailMessage
    {
        public string To { get; set; }
        public string Bcc { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsHtml { get; set; }
    }

    public interface IEMailService
    {
        void SendEmail(EmailMessage message, string account);
    }
}
