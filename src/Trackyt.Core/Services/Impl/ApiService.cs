using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyt.Core.DAL.Repositories;
using Trackyt.Core.DAL.DataModel;

namespace Trackyt.Core.Services.Impl
{
    public class ApiService : IApiService
    {
        private IUsersRepository _users;
        private IHashService _hash;

        public ApiService(IUsersRepository users, IHashService hash)
        {
            _users = users;
            _hash = hash;
        }

        public User GetUserByApiToken(string apiToken)
        {
            var user = _users.Users.Where(u => u.ApiToken == apiToken).SingleOrDefault();
            if (user == null)
            {
                throw new Exception(string.Format("Can't find user with token: {0}", apiToken));
            }

            return user;
        }

        public string GetApiToken(string email, string password)
        {
            var user = _users.Users.Where(u => u.Email == email).SingleOrDefault();
            if (user != null && _hash.ValidateMD5Hash(password, user.PasswordHash))
            {
                return user.ApiToken;
            }

            return null;
        }
    }
}
