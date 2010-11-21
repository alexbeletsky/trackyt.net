using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Infrastructure.Security;
using Trackyt.Core.DAL.Repositories;
using Trackyt.Core.DAL.Extensions;

namespace Web.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private IUsersRepository _users;
        private IFormsAuthentication _forms;

        public AuthenticationService(IUsersRepository users, IFormsAuthentication forms)
        {
            _users = users;
            _forms = forms;
        }

        public bool Authenticate(string email, string password)
        {
            var user = _users.Users.WithEmail(email);
            if (user != null && user.Password == password)
            {
                _forms.SetAuthCookie(email, false);
                return true;
            }

            return false;
        }


        public int GetUserId(string email)
        {
            var user = _users.Users.WithEmail(email);
            return (user == null ? 0 : user.Id);  
        }
    }
}