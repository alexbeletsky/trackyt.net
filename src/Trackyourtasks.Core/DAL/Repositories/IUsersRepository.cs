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
        /// Find user by Id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        User FindUserById(int id);

        /// <summary>
        /// Find user by Email
        /// </summary>
        /// <param name="email">User email</param>
        /// <returns></returns>
        User FindUserByEmail(string email);

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
