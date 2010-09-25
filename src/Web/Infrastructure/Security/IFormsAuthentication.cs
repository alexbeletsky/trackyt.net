using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Infrastructure.Security
{
    public interface IFormsAuthentication
    {
        void SetAuthCookie(string email, bool persistant);
        string GetLoggedUser(); 
    }
}