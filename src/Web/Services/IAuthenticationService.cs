using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Services
{
    public interface IAuthenticationService
    {
        bool Authenticate(string email, string password);
    }
}