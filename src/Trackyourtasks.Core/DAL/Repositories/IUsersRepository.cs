using System;
using System.Linq;
using System.Text;
using Trackyourtasks.Core.DAL.DataModel;

namespace Trackyourtasks.Core.DAL.Repositories
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
        IQueryable<User> GetUsers();

        /// <summary>
        /// Save user
        /// Creates new record and initialize Id for new objects and update exiting objects
        /// </summary>
        /// <param name="user">User object</param>
        void SaveUser(User user);

        /// <summary>
        /// Deletes user
        /// </summary>
        /// <param name="user">User object</param>
        void DeleteUser(User user);
    }
}
