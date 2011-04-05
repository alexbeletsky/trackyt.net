using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyt.Core.DAL.DataModel;

namespace Trackyt.Core.DAL.Extensions
{
    public static class TasksExtensions
    {
        public static Task WithId(this IQueryable<Task> tasks, int id)
        {
            return tasks.Where(t => t.Id == id).SingleOrDefault();
        }

        public static IQueryable<Task> WithUserId(this IQueryable<Task> tasks, int id)
        {
            return tasks.Where(t => t.UserId == id && !t.Done);
        }

        public static IQueryable<Task> WithUserIdAndDone(this IQueryable<Task> tasks, int id)
        {
            return tasks.Where(t => t.UserId == id && t.Done);
        }
    }
}
