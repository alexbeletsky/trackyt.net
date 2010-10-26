using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyourtasks.Core.DAL.DataModel;
using Trackyourtasks.Core.DAL.Extensions;
using System.Data.Linq;

namespace Trackyourtasks.Core.DAL.Repositories.Impl
{
    public class UsersRepository : IUsersRepository
    {
        private TrackyDataContext _context;

        public UsersRepository()
            : this(new TrackyDataContext())
        {

        }

        //used in unit tests
        public UsersRepository(TrackyDataContext  context)
        {
            _context = context;
        }

        #region IUsersRepository Members

        public IQueryable<User> Users
        {
            get
            {
                return _context.Users.AsQueryable();
            }
        }

        public void SaveUser(User user)
        {
            if (Users.WithEmail(user.Email) != null)
                throw new DuplicateKeyException(user);

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
