using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyourtasks.Core.DAL.Repositories;

namespace Trackyourtasks.Core.Tests.Mocks
{
    public class TasksRepositoryMock : ITasksRepository
    {
        private IList<DAL.DataModel.Task> _tasksRepository = new List<DAL.DataModel.Task>();

        public void SaveTask(DAL.DataModel.Task task)
        {
            _tasksRepository.Add(task);
        }

        public void DeleteTask(DAL.DataModel.Task task)
        {
            _tasksRepository.Remove(task);
        }

        public IQueryable<DAL.DataModel.Task> GetTasks()
        {
            return _tasksRepository.AsQueryable();
        }
    }
}
