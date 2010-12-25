using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyt.Core.DAL.Repositories;

namespace Trackyt.Core.Services
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

        public int GetUserIdByApiToken(string apiToken)
        {
            var user = _users.Users.Where(u => u.ApiToken == apiToken).SingleOrDefault();
            if (user == null)
            {
                return 0;
            }

            return user.Id;
        }

        public string GetApiToken(string email, string password)
        {
            var user = _users.Users.Where(u => u.Email == email).SingleOrDefault();
            if (_hash.ValidateMD5Hash(password, user.PasswordHash))
            {
                return user.ApiToken;
            }

            return null;
        }
    }
}
