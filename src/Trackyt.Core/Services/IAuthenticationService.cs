using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trackyt.Core.Services
{
    public interface IAuthenticationService
    {
        bool Authenticate(string email, string password);
        int GetUserId(string email);
        bool CreateUser(string email, string password, bool temp);
    }
}