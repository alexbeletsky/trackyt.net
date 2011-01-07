using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Trackyt.Core.DAL.DataModel;
using Trackyt.Core.DAL.Extensions;
using Trackyt.Core.DAL.Repositories;
using Trackyt.Core.Services;
using Web.API.v11.Model;
using Web.Infrastructure.Error;
using Web.Infrastructure.Exceptions;

namespace Web.API.v11.Controllers
{
    [HandleJsonError]
    public class ApiV11Controller : Controller
    {
        private IApiService _api;
        private ITasksRepository _tasks;
        private IMappingEngine _mapper;

        public ApiV11Controller(IApiService auth, ITasksRepository repository, IMappingEngine mapper)
        {
            _api = auth;
            _tasks = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public JsonResult Authenticate(string email, string password)
        {
            CheckArgumentNotNullOrEmpty(email, "email");
            CheckArgumentNotNullOrEmpty(password, "password");

            var success = true;
            var apiToken = _api.GetApiToken(email, password);

            if (apiToken == null)
            {
                throw new UserNotAuthorized();
            }

            return Json(
                new
                {
                    success = success,
                    data = new { apiToken = apiToken }
                });
        }

        // Tasks

        // GET tasks/all
        [HttpGet]
        public JsonResult All(string apiToken)
        {
            CheckArgumentApiToken(apiToken);
            
            var userId = CheckAuthorization(apiToken);
            var tasks = CreateTasksList(userId);

            return Json(
                new
                {
                    success = true,
                    data = new { tasks = tasks }
                },
                JsonRequestBehavior.AllowGet);
        }

        // POST tasks/add
        [HttpPost]
        public JsonResult Add(string apiToken, string description)
        {
            CheckArgumentApiToken(apiToken);
            CheckArgumentNotNullOrEmpty(description, "description");

            var userId = CheckAuthorization(apiToken);
            var task = new Task { Description = description, UserId = userId, Status = (int)TaskStatus.None };
            _tasks.Save(task);

            return Json(
                new
                {
                    success = true,
                    data = new { task = CreateTaskDescriptor(task) }
                });
        }

        // DELETE tasks/delete
        [HttpDelete]
        public JsonResult Delete(string apiToken, int taskId)
        {
            CheckArgumentApiToken(apiToken);
            CheckArgumentLessThanZero(taskId, "taskId");

            var userId = CheckAuthorization(apiToken);
            var task = _tasks.Tasks.WithId(taskId);

            CheckTaskNotNull(taskId, task);
            _tasks.Delete(task);

            return Json(
                new
                {
                    success = true,
                    data = new { id = taskId }
                });
        }

        // PUT tasks/start

        [HttpPut]
        public JsonResult Start(string apiToken, int taskId)
        {
            CheckArgumentApiToken(apiToken);
            CheckArgumentLessThanZero(taskId, "taskId");

            var userId = CheckAuthorization(apiToken);
            var task = _tasks.Tasks.WithId(taskId);

            CheckTaskNotNull(taskId, task);
            StartAndSave(task);

            return Json(
                new
                {
                    success = true,
                    data = CreateTaskDescriptor(task) 
                });
        }

        // PUT tasks/stop

        [HttpPut]
        public JsonResult Stop(string apiToken, int taskId)
        {
            CheckArgumentApiToken(apiToken);
            CheckArgumentLessThanZero(taskId, "taskId");

            var userId = CheckAuthorization(apiToken);
            var task = _tasks.Tasks.WithId(taskId);

            CheckTaskNotNull(taskId, task);
            StopAndSave(task);

            return Json(
                new
                {
                    success = true,
                    data = CreateTaskDescriptor(task)
                });
        }


        // PUT tasks/start/all

        [HttpPut]
        public JsonResult StartAll(string apiToken)
        {
            CheckArgumentApiToken(apiToken);

            var userId = CheckAuthorization(apiToken);
            var allTasks = _tasks.Tasks.WithUserId(userId);

            foreach (var task in allTasks)
            {
                StartAndSave(task);
            }

            return Json(
                new
                {
                    success = true,
                    data = (string)null
                });
        }

        // PUT tasks/stop/all

        [HttpPut]
        public JsonResult StopAll(string apiToken)
        {
            CheckArgumentApiToken(apiToken);

            var userId = CheckAuthorization(apiToken);
            var allTasks = _tasks.Tasks.WithUserId(userId);

            foreach (var task in allTasks)
            {
                StopAndSave(task);
            }

            return Json(
                new
                {
                    success = true,
                    data = (string)null
                });
        }

        private IList<TaskDescriptor> CreateTasksList(int userId)
        {
            return _tasks.Tasks.WithUserId(userId).Select(t => CreateTaskDescriptor(t)).ToList();
        }

        private TaskDescriptor CreateTaskDescriptor(Task t)
        {
            return new TaskDescriptor
            {
                id = t.Id,
                description = t.Description,
                createdDate = t.CreatedDate,
                startedDate = t.StartedDate,
                stoppedDate = t.StoppedDate,
                status = t.Status,
                spent = GetTaskActualWork(t)
            };
        }

        private int GetTaskActualWork(Task t)
        {
            var actualWork = t.ActualWork;

            if (t.Status == (int)TaskStatus.Started)
            {
                actualWork += GetDifferenceInSeconds(t.StartedDate, DateTime.UtcNow);
            }

            return actualWork;
        }

        private int GetDifferenceInSeconds(DateTime? start, DateTime? stop)
        {
            if (start == null)
            {
                return 0;
            }

            if (stop == null)
            {
                return Convert.ToInt32(Math.Floor((DateTime.UtcNow - start).Value.TotalSeconds));
            }

            return Convert.ToInt32(Math.Floor((stop - start).Value.TotalSeconds));
        }

        private void StartAndSave(Task task)
        {
            if (task.Status == (int)TaskStatus.None || task.Status == (int)TaskStatus.Stopped)
            {
                task.Status = (int)TaskStatus.Started;
                task.StartedDate = DateTime.UtcNow;
                task.StoppedDate = null;
                _tasks.Save(task);
            }
        }

        private void StopAndSave(Task task)
        {
            if (task.Status == (int)TaskStatus.Started)
            {
                task.Status = (int)TaskStatus.Stopped;
                task.StoppedDate = DateTime.UtcNow;
                task.ActualWork += GetDifferenceInSeconds(task.StartedDate, task.StoppedDate);

                _tasks.Save(task);
            }
        }

        private void CheckArgumentNotNullOrEmpty(string value, string name)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(name);
            }
        }

        private void CheckArgumentApiToken(string apiToken)
        {
            if (string.IsNullOrEmpty(apiToken) || apiToken.Length != 32)
            {
                throw new ArgumentException("Provided Api token has wrong format.");
            }
        }

        private void CheckArgumentLessThanZero(int value, string name)
        {
            if (value < 0)
            {
                throw new ArgumentException("Provided value could not be less than zero.", name);
            }
        }

        private int CheckAuthorization(string apiToken)
        {
            var userId = _api.GetUserIdByApiToken(apiToken);

            if (userId == 0)
            {
                throw new UserNotAuthorized();
            }

            return userId;
        }

        private static void CheckTaskNotNull(int taskId, Task task)
        {
            if (task == null)
            {
                throw new Exception(string.Format("Task with id: {0} does not exists. Operation failed.", taskId));
            }
        }
    }
}
