using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyourtasks.Core.DAL.DataModel;

namespace Trackyourtasks.Core.DAL.Repositories.Impl
{
    public class UsersRepository : IUsersRepository
    {
        private TrackYourTasksDataContext _context;

        public UsersRepository()
            : this(new TrackYourTasksDataContext())
        {

        }

        //used in unit tests
        public UsersRepository(TrackYourTasksDataContext  context)
        {
            _context = context;
        }

        #region IUsersRepository Members

        public User FindUserById(int id)
        {
            return _context.Users.Where(u => u.Id == id).SingleOrDefault();
        }

        public User FindUserByEmail(string email)
        {
            return _context.Users.Where(u => u.Email == email).SingleOrDefault();
        }

        public void SaveUser(User user)
        {
            if (user.Id == 0)
            {
                _context.Users.InsertOnSubmit(user);
            }
            _context.SubmitChanges();
        }

        public void DeleteUser(User user)
        {
            _context.Users.DeleteOnSubmit(user);
            _context.SubmitChanges();
        }

        #endregion
    }
}
