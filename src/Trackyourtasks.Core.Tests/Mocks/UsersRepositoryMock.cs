using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyourtasks.Core.DAL;
using Trackyourtasks.Core.DAL.DataModel;

namespace Trackyourtasks.Core.BLL.Tests.Mocks
{
    class UsersRepositoryMock : IUsersRepository
    {
        private IList<User> _users = new List<User>();
        private bool _failOnRegister;

        public UsersRepositoryMock()
        {

        }

        public UsersRepositoryMock(bool fail)
        {
            _failOnRegister = fail;
        }

        #region IUsersRepository Members

        public User FindUserById(int id)
        {
            throw new NotImplementedException();
        }

        public User FindUserByEmail(string email)
        {
            return (from user in _users where user.Email == email select user).SingleOrDefault();
        }

        public void InsertUser(User user)
        {
            if (_failOnRegister)
                throw new Exception();

            _users.Add(user);    
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
