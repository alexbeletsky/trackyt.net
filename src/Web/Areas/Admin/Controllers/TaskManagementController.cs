using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trackyt.Core.DAL.Repositories;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Controllers
{
    public class TaskManagementController : Controller
    {
        private ITasksRepository _tasksRepository;

        public TaskManagementController(ITasksRepository tasksRepository)
        {
            _tasksRepository = tasksRepository;
        }

        public ActionResult Summary()
        {
            var totalTasks = _tasksRepository.Tasks.Count();
            var totalLoggedTime = _tasksRepository.Tasks.Sum(t => t.ActualWork);

            return View(new TaskSummaryModel { TotalTasks = totalTasks, TotalLoggedTime = totalLoggedTime });
        }

    }
}
