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

        public IQueryable<Task> GetTasks()
        {
            return _context.Tasks;
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
