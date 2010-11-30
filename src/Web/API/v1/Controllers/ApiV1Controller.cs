using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trackyt.Core.DAL.Repositories.Impl;
using Trackyt.Core.DAL.DataModel;
using Trackyt.Core.DAL.Extensions;
using Trackyt.Core.DAL.Repositories;
using AutoMapper;
using Web.API.v1.Model;
using Web.Infrastructure.Security;
using Trackyt.Core.Services;

namespace Web.API.v1.Controllers
{
    //Used AutoMapper, good example found here:
    //http://richarddingwall.name/2009/08/18/asp-net-mvc-tdd-and-automapper/

    // Used JSON and APS.net MVC
    // http://haacked.com/archive/2010/04/15/sending-json-to-an-asp-net-mvc-action-method-argument.aspx

    // Information used, how to unit test JSON response
    // http://www.heartysoft.com/post/2010/05/25/ASPNET-MVC-Unit-Testing-JsonResult-Returning-Anonymous-Types.aspx

    //TODO: error handling
    //TODO: HandleError have to be added here (perhaps, this not only class to have it)
    // Information
    // http://forums.asp.net/p/1471123/3407713.aspx
    // http://weblogs.asp.net/scottgu/archive/2008/07/14/asp-net-mvc-preview-4-release-part-1.aspx

    public class ApiV1Controller : Controller
    {
        private IAuthenticationService _auth;
        private ITasksRepository _repository;
        private IMappingEngine _mapper;

        public ApiV1Controller(IAuthenticationService auth, ITasksRepository repository, IMappingEngine mapper)
        {
            _auth = auth;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public JsonResult Authenticate(string email, string password)
        {
            var success = false;
            var userId = 0;

            if (_auth.Authenticate(email, password))
            {
                success = true;
                userId = _auth.GetUserId(email);
            }

            return Json(
                new { success = success, 
                    data = new { userId = (userId > 0 ? userId.ToString() : (string)null) } 
                });
        }

        [HttpPost]
        [TrackyAuthorizeAttribute(LoginController = "Login")]
        public JsonResult GetAllTasks(int id)
        {
            var tasksQuery = _repository.Tasks.WithUserId(id);
            
            return Json(
                new
                {
                    success = true,
                    data = new { tasks = tasksQuery.Select(t => _mapper.Map<Task, TaskDto>(t)).ToList() }
                });
        }

        [HttpPost]
        [TrackyAuthorizeAttribute(LoginController = "Login")]
        public JsonResult Submit(int id, IList<TaskDto> tasks)
        {
            var newTasks = 0;
            var updateTasks = 0;

            foreach (var taskData in tasks)
            {
                Task task = null;
                if (taskData.Id == 0)
                {
                    newTasks++;
                    task = new Task();
                }
                else
                {
                    updateTasks++;
                    task = _repository.Tasks.WithId(taskData.Id);
                }
                
                task.UserId = id;
                task.Number = taskData.Number;
                task.Description = taskData.Description;
                task.ActualWork = taskData.ActualWork;

                _repository.SaveTask(task);
            }

            return Json(
                new
                {
                    success = true,
                    data = new { newTasks = newTasks, updatedTasks = updateTasks, deletedTasks = 0 }
                });
                    
        }

        [HttpPost]
        [TrackyAuthorizeAttribute(LoginController = "Login")]
        public JsonResult Delete(int id, IList<TaskDto> tasks)
        {
            var deletedTasks = 0;
            foreach (var taskData in tasks)
            {
                if (taskData.Id != 0)
                {
                    deletedTasks++;

                    var taskToDelete = _repository.Tasks.WithId(taskData.Id);
                    _repository.DeleteTask(taskToDelete);
                }
            }

            return Json(
                new
                {
                    success = true,
                    data = new { newTasks = 0, updatedTasks = 0, deletedTasks = deletedTasks }
                });
        }

    }
}
