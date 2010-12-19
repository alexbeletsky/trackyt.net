using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trackyt.Core.Security
{
    public interface IFormsAuthentication
    {
        void SetAuthCookie(string email, bool persistant);
        
        string GetLoggedUserEmail();
        string GeneratePassword();
    }
}