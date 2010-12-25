using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyt.Core.DAL;
using Trackyt.Core.DAL.DataModel;
using Trackyt.Core.DAL.Repositories;

namespace Trackyt.Core.Tests.Mocks
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

        public IQueryable<User> Users
        {
            get
            {
                return _users.AsQueryable();
            }
        }

        public void Save(User user)
        {
            if (_failOnRegister)
                throw new Exception();

            _users.Add(user);    
        }

        public void Delete(User user)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
