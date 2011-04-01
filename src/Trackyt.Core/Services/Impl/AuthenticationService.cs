using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trackyt.Core.DAL.Repositories;
using Trackyt.Core.DAL.Extensions;
using Trackyt.Core.Security;
using Trackyt.Core.DAL.DataModel;

namespace Trackyt.Core.Services.Impl
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

        public int GetUserIdByEmail(string email)
        {
            var user = _users.Users.WithEmail(email);
            return (user == null ? 0 : user.Id);  
        }

        public bool RegisterNewUser(string email, string password)
        {
            if (_users.Users.WithEmail(email) != null)
            {
                return false;
            }

            var user = CreateUserWithTempFlag(email, password, false);

            SaveAndAuthenticate(email, password, user);

            return true;
        }

        public bool RegisterTemporaryUser(string email, string password)
        {
            if (_users.Users.WithEmail(email) != null)
            {
                return false;
            }

            var user = CreateUserWithTempFlag(email, password, true);

            SaveAndAuthenticate(email, password, user);

            return true;
        }

        private User CreateUserWithTempFlag(string email, string password, bool temp)
        {
            var user = new User
            {
                Email = email,
                PasswordHash = _hash.CreateMD5Hash(password),
                ApiToken = _hash.CreateApiToken(email, password),
                Temp = temp
            };

            return user;
        }

        private void SaveAndAuthenticate(string email, string password, User user)
        {
            _users.Save(user);
            Authenticate(email, password);
        }
    }
}