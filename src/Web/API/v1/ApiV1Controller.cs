using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trackyourtasks.Core.DAL.Repositories.Impl;
using Trackyourtasks.Core.DAL.DataModel;
using Trackyourtasks.Core.DAL.Extensions;

namespace Web.Controllers
{
    //TODO: error handling
    //TODO: authentication (try to unit test)
    //TODO: write tests for TasksController
    //TODO: write a model class for Task object
    //TODO: provide an API with registered route
    [Authorize]
    public class ApiV1Controller : Controller
    {
        [HttpPost]
        public JsonResult GetAllTasks(int id)
        {
            var repository = new TasksRepository();
            return Json(repository.GetTasks().WithUserId(id).ToList());
        }

        //TODO: fix it, to receive UserId
        [HttpPost]
        public JsonResult Submit(IList<Task> tasks)
        {
            var repository = new TasksRepository();
            foreach (var task in tasks)
            {
                if (task.Id != 0)
                {
                    var taskToUpdate = repository.GetTasks().WithId(task.Id);
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

            return Json(null);
        }

        //TODO: fix it, to receive UserId
        [HttpPost]
        public JsonResult Delete(IList<Task> tasks)
        {
            var repository = new TasksRepository();
            foreach (var task in tasks)
            {
                if (task.Id != 0)
                {
                    var taskToDelete = repository.GetTasks().WithId(task.Id);
                    repository.DeleteTask(taskToDelete);
                }
            }

            return Json(null);
        }

    }
}
