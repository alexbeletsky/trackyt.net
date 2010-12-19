using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Trackyt.Core.Security
{
    [CoverageExcludeAttribute]
    public class TrackyFormsAuthentication : IFormsAuthentication
    {
        public void SetAuthCookie(string email, bool persistant)
        {
            FormsAuthentication.SetAuthCookie(email, persistant);
        }

        public string GetLoggedUserEmail()
        {
            return HttpContext.Current.User.Identity.Name;
        }

        public string GeneratePassword()
        {
            return Membership.GeneratePassword(8, 1);
        }
    }
}