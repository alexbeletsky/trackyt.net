using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trackyt.Core.Services
{
    public interface IAuthenticationService
    {
        bool Authenticate(string email, string password);
        int GetUserIdByEmail(string email);

        // TODO: refactor method to do not accept boolean flags
        bool RegisterNewUser(string email, string password, bool temp);
    }
}