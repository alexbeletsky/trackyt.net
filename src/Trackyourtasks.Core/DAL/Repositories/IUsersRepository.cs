using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyourtasks.Core.DAL.DataModel;

namespace Trackyourtasks.Core.DAL
{
    public interface IUsersRepository
    {
        User FindUserById(int id);
        User FindUserByEmail(string email);

        void InsertUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
