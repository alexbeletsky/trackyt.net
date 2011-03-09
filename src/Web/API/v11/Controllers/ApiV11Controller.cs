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
        private IDateTimeProviderService _dateTime;

        public ApiV11Controller(IApiService auth, ITasksRepository repository, IDateTimeProviderService date)
        {
            _api = auth;
            _tasks = repository;
            _dateTime = date;
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
                throw new UserNotAuthorizedException();
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
            
            var userId = GetUserIdByApiToken(apiToken);
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

            var userId = GetUserIdByApiToken(apiToken);
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

            var userId = GetUserIdByApiToken(apiToken);
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

            var userId = GetUserIdByApiToken(apiToken);
            var task = _tasks.Tasks.WithId(taskId);

            CheckTaskNotNull(taskId, task);
            StartAndSave(task);

            return Json(
                new
                {
                    success = true,
                    data = new { task = CreateTaskDescriptor(task) }
                });
        }

        // PUT tasks/stop

        [HttpPut]
        public JsonResult Stop(string apiToken, int taskId)
        {
            CheckArgumentApiToken(apiToken);
            CheckArgumentLessThanZero(taskId, "taskId");

            var userId = GetUserIdByApiToken(apiToken);
            var task = _tasks.Tasks.WithId(taskId);

            CheckTaskNotNull(taskId, task);
            StopAndSave(task);

            return Json(
                new
                {
                    success = true,
                    data = new { task = CreateTaskDescriptor(task) }
                });
        }


        // PUT tasks/start/all

        [HttpPut]
        public JsonResult StartAll(string apiToken)
        {
            CheckArgumentApiToken(apiToken);

            var userId = GetUserIdByApiToken(apiToken);
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

            var userId = GetUserIdByApiToken(apiToken);
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

        [HttpPut]
        public ActionResult UpdatePosition(string apiToken, int taskId, int position)
        {
            CheckArgumentApiToken(apiToken);

            var userId = GetUserIdByApiToken(apiToken);
            var task = _tasks.Tasks.WithId(taskId);

            CheckTaskNotNull(taskId, task);

            task.Position = position;
            _tasks.Save(task);

            return Json(
                new
                {
                    success = true,
                    data = new { task = CreateTaskDescriptor(task) }
                });
        }

        [HttpPut]
        public ActionResult UpdateDescription(string apiToken, int taskId, string description)
        {
            CheckArgumentApiToken(apiToken);

            var userId = GetUserIdByApiToken(apiToken);
            var task = _tasks.Tasks.WithId(taskId);

            CheckTaskNotNull(taskId, task);
            
            task.Description = description;
            _tasks.Save(task);

            return Json(
                new
                {
                    success = true,
                    data = new { task = CreateTaskDescriptor(task) }
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
                spent = GetTaskActualWork(t),
                position = t.Position
            };
        }

        private int GetTaskActualWork(Task t)
        {
            var actualWork = t.ActualWork;

            if (t.Status == (int)TaskStatus.Started)
            {
                actualWork += GetDifferenceInSeconds(t.StartedDate, _dateTime.UtcNow);
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
                return Convert.ToInt32(Math.Floor((_dateTime.UtcNow - start).Value.TotalSeconds));
            }

            return Convert.ToInt32(Math.Floor((stop - start).Value.TotalSeconds));
        }

        private void StartAndSave(Task task)
        {
            if (task.Status == (int)TaskStatus.None || task.Status == (int)TaskStatus.Stopped)
            {
                task.Status = (int)TaskStatus.Started;
                task.StartedDate = _dateTime.UtcNow;
                task.StoppedDate = null;
                _tasks.Save(task);
            }
        }

        private void StopAndSave(Task task)
        {
            if (task.Status == (int)TaskStatus.Started)
            {
                task.Status = (int)TaskStatus.Stopped;
                task.StoppedDate = _dateTime.UtcNow;
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

        private int GetUserIdByApiToken(string apiToken)
        {
            var userId = _api.GetUserIdByApiToken(apiToken);

            if (userId == 0)
            {
                throw new UserNotAuthorizedException();
            }

            return userId;
        }

        // TODO: get rid of that check. Repository must throw such exception, in case of task does not exist
        private static void CheckTaskNotNull(int id, Task task)
        {
            if (task == null)
            {
                throw new Exception(string.Format("Task with id: {0} does not exists.", id));
            }
        }
    }
}
