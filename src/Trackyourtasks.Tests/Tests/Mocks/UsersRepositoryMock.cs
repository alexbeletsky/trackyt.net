using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyourtasks.Core.DAL;
using Trackyourtasks.Core.DAL.DataModel;
using Trackyourtasks.Core.DAL.Repositories;

namespace Trackyourtasks.Core.Tests.Mocks
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

        public IQueryable<User> GetUsers()
        {
            return _users.AsQueryable();
        }

        public void SaveUser(User user)
        {
            if (_failOnRegister)
                throw new Exception();

            _users.Add(user);    
        }

        public void DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
