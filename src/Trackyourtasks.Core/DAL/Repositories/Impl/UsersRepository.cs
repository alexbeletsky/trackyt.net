using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyourtasks.Core.DAL.DataModel;

namespace Trackyourtasks.Core.DAL
{
    public class UsersRepository : IUsersRepository
    {
        TrackYourTasksDataContext _context = new TrackYourTasksDataContext();

        #region IUsersRepository Members

        public User FindUserById(int id)
        {
            throw new NotImplementedException();
        }

        public User FindUserByEmail(string email)
        {
            return (from user in _context.Users where user.Email == email select user).SingleOrDefault();
        }

        public void InsertUser(User user)
        {
            _context.Users.InsertOnSubmit(user);
            _context.SubmitChanges();
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
