using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyourtasks.Core.DAL.DataModel;

namespace Trackyourtasks.Core.DAL.Extensions
{
    public static class TasksExtensions
    {
        public static Task WithId(this IQueryable<Task> tasks, int id)
        {
            return tasks.Where(t => t.Id == id).SingleOrDefault();
        }

        public static Task WithUserId(this IQueryable<Task> tasks, int id)
        {
            return tasks.Where(t => t.UserId == id).SingleOrDefault();
        }

    }
}
