using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyourtasks.Core.DAL.Repositories;
using Trackyourtasks.Core.DAL.DataModel;

namespace Trackyourtasks.Core.BLL.Tasks
{
    public class AddTask
    {
        private ITasksRepository _repository;

        public AddTask(ITasksRepository repository)
        {
            _repository = repository;
        }

        public void Add(Task task)
        {
            _repository.SaveTask(task);
        }
    }
}
