using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trackyourtasks.Core.DAL.Repositories.Impl;
using Trackyourtasks.Core.DAL.DataModel;
using Trackyourtasks.Core.DAL.Extensions;
using Trackyourtasks.Core.DAL.Repositories;
using AutoMapper;
using Web.API.v1.Model;

namespace Web.Controllers
{
    //Used AutoMapper, good example found here:
    //http://richarddingwall.name/2009/08/18/asp-net-mvc-tdd-and-automapper/

    //TODO: error handling
    //TODO: authentication (try to unit test)
    [Authorize]
    public class ApiV1Controller : Controller
    {
        private ITasksRepository _repository;
        private IMappingEngine _mapper;

        public ApiV1Controller(ITasksRepository repository, IMappingEngine mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public JsonResult GetAllTasks(int id)
        {
            var tasksQuery = _repository.GetTasks().WithUserId(id);
            return Json(tasksQuery.Select(t => _mapper.Map<Task, TaskDto>(t)).ToList());
        }

        //TODO: fix it, to receive UserId
        [HttpPost]
        public JsonResult Submit(int id, IList<TaskDto> tasks)
        {
            foreach (var taskData in tasks)
            {
                var task = taskData.Id == 0 ? new Task() : _repository.GetTasks().WithId(taskData.Id);
                task.UserId = id;
                task.Number = taskData.Number;
                task.Description = taskData.Description;
                task.ActualWork = taskData.ActualWork;

                _repository.SaveTask(task);
            }

            return Json(null);
        }

        //TODO: fix it, to receive UserId
        [HttpPost]
        public JsonResult Delete(int id, IList<TaskDto> tasks)
        {
            foreach (var taskData in tasks)
            {
                if (taskData.Id != 0)
                {
                    var taskToDelete = _repository.GetTasks().WithId(taskData.Id);
                    _repository.DeleteTask(taskToDelete);
                }
            }

            return Json(null);
        }

    }
}
