using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyt.Core.DAL.DataModel;

namespace Trackyt.Core.DAL.Repositories
{
    /// <summary>
    /// Repository of Tasks
    /// </summary>
    public interface ITasksRepository
    {
        /// <summary>
        /// Gets all tasks
        /// </summary>
        /// <returns></returns>
        IQueryable<Task> Tasks { get; }

        /// <summary>
        /// Saves or updates task
        /// </summary>
        /// <param name="task">Task object</param>
        void Save(Task task);

        /// <summary>
        /// Deletes task
        /// </summary>
        /// <param name="task">Task object</param>
        void Delete(Task task);
    }
}
