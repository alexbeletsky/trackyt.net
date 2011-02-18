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

        //bool RegisterNewUser(string email, string password, bool temp);
        bool RegisterNewUser(string email, string password);
        bool RegisterTemporaryUser(string email, string password);
    }
}