using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyourtasks.Core.DAL.DataModel;

namespace Trackyourtasks.Core.DAL.Repositories.Impl
{
    public class TasksRepository : ITasksRepository
    {
        private TrackYourTasksDataContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        public TasksRepository() : this(new TrackYourTasksDataContext())
        {

        }

        /// <summary>
        /// Constructor used in unit tests
        /// </summary>
        /// <param name="context">Context</param>
        public TasksRepository(TrackYourTasksDataContext context)
        {
            _context = context;
        }

        public IEnumerable<Task> GetAllTasks()
        {
            return _context.Tasks.Select(t => t);
        }

        public Task FindTaskById(int id)
        {
            return _context.Tasks.Where(t => t.Id == id).SingleOrDefault();
        }

        public Task FindTaskByUserId(int id)
        {
            return _context.Tasks.Where(t => t.UserId == id).SingleOrDefault();
        }

        public void SaveTask(Task task)
        {
            if (task.Id == 0)
            {
                _context.Tasks.InsertOnSubmit(task);
            }
            _context.SubmitChanges();
        }

        public void DeleteTask(Task task)
        {
            _context.Tasks.DeleteOnSubmit(task);
            _context.SubmitChanges();
        }
    }
}
