using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Trackyt.Core.Services.Impl
{
    public class RedirectService : IRedirectService
    {
        public RedirectResult ToDashboard(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException(email);
            }

            return new RedirectResult("~/user/" + email);
        }

        public RedirectResult ToRegistrationSuccess()
        {
            return new RedirectResult("~/registration/success");
        }

        public RedirectResult ToUrl(string url)
        {
            return new RedirectResult(url);            
        }
    }
}
