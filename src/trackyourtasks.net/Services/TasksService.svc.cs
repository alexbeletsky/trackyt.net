using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using Trackyourtasks.Core.DAL.Repositories.Impl;
using Trackyourtasks.Core.DAL.DataModel;

namespace trackyourtasks.net.Services
{
    [ServiceContract(Namespace = "Services")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class TasksService
    {
        [OperationContract]
        public IList<Task> GetAllTasks()
        {
            var repository = new TasksRepository();
            return repository.GetAllTasks().ToList();
        }

        [OperationContract]
        public void Submit(IList<Task> tasks)
        {
            var repository = new TasksRepository();

            foreach (var task in tasks)
            {
                if (task.Id != 0)
                {
                    var taskToUpdate = repository.FindTaskById(task.Id);
                    taskToUpdate.ActualWork = task.ActualWork;
                    taskToUpdate.Description = task.Description;
                    taskToUpdate.Status = task.Status;

                    repository.SaveTask(taskToUpdate);
                }
                else
                {
                    repository.SaveTask(task);
                }

                
            }
        }
    }
}
