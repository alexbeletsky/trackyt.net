using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trackyt.Core.DAL.Repositories;
using Trackyt.Core.DAL.Extensions;
using Trackyt.Core.Security;
using Trackyt.Core.DAL.DataModel;

namespace Trackyt.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private IUsersRepository _users;
        private IFormsAuthentication _forms;
        private IHashService _hash;

        public AuthenticationService(IUsersRepository users, IFormsAuthentication forms, IHashService hash)
        {
            _users = users;
            _forms = forms;
            _hash = hash;
        }

        public bool Authenticate(string email, string password)
        {
            var user = _users.Users.WithEmail(email);
            if (user != null && _hash.ValidateMD5Hash(password, user.PasswordHash))
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

        public bool CreateUser(string email, string password, bool temp)
        {
            if (_users.Users.WithEmail(email) != null)
            {
                return false;
            }

            var user = new User
            {
                Email = email,
                PasswordHash = _hash.CreateMD5Hash(password),
                ApiToken = _hash.CreateApiToken(email, password),
                Temp = temp
            };

            _users.SaveUser(user);

            Authenticate(email, password);

            return true;
        }
    }
}