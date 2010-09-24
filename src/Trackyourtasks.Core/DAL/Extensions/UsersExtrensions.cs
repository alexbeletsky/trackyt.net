using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyourtasks.Core.DAL.DataModel;

namespace Trackyourtasks.Core.DAL.Extensions
{
    public static class UsersExtensions
    {
        public static User WithEmail(this IQueryable<User> users, string email)
        {
            return users.Where(u => u.Email == email).SingleOrDefault();
        }

        public static User WithId(this IQueryable<User> users, int id)
        {
            return users.Where(u => u.Id == id).SingleOrDefault();
        }
    }
}
