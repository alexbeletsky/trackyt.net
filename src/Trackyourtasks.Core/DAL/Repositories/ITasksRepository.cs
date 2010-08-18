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
        IEnumerable<Task> GetAllTasks();

        /// <summary>
        /// Finds task by Id
        /// </summary>
        /// <param name="id">Task Id</param>
        /// <returns></returns>
        Task FindTaskById(int id);

        /// <summary>
        /// Finds task by referenced user Id
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns></returns>
        Task FindTaskByUserId(int id);

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
