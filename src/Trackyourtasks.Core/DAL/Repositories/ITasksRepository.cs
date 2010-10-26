using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyourtasks.Core.DAL.DataModel;

namespace Trackyourtasks.Core.DAL.Repositories
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
        void SaveTask(Task task);

        /// <summary>
        /// Deletes task
        /// </summary>
        /// <param name="task">Task object</param>
        void DeleteTask(Task task);
    }
}
