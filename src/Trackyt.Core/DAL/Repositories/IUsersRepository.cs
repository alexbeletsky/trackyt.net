using System;
using System.Linq;
using System.Text;
using Trackyt.Core.DAL.DataModel;

namespace Trackyt.Core.DAL.Repositories
{
    /// <summary>
    /// Repository of Users
    /// </summary>
    public interface IUsersRepository
    {
        /// <summary>
        /// Gets all users from repository
        /// </summary>
        /// <returns></returns>
        IQueryable<User> Users { get; }

        /// <summary>
        /// Creates new record and initialize Id for new objects and update exiting objects
        /// </summary>
        /// <param name="user">User object</param>
        void Save(User user);

        /// <summary>
        /// Deletes user
        /// </summary>
        /// <param name="user">User object</param>
        void Delete(User user);
    }
}
