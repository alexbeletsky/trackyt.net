//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Mvc;
//using AutoMapper;
//using Trackyt.Core.DAL.DataModel;
//using Trackyt.Core.DAL.Extensions;
//using Trackyt.Core.DAL.Repositories;
//using Trackyt.Core.Services;
//using Web.API.v1.Model;
//using Web.Infrastructure.Security;

//namespace Web.API.v1.Controllers
//{
//    //Used AutoMapper, good example found here:
//    //http://richarddingwall.name/2009/08/18/asp-net-mvc-tdd-and-automapper/

//    // Used JSON and APS.net MVC
//    // http://haacked.com/archive/2010/04/15/sending-json-to-an-asp-net-mvc-action-method-argument.aspx

//    // Information used, how to unit test JSON response
//    // http://www.heartysoft.com/post/2010/05/25/ASPNET-MVC-Unit-Testing-JsonResult-Returning-Anonymous-Types.aspx

//    // HandleError Information
//    // http://forums.asp.net/p/1471123/3407713.aspx
//    // http://weblogs.asp.net/scottgu/archive/2008/07/14/asp-net-mvc-preview-4-release-part-1.aspx

//    // NOTE: This controller is no longer used, see ApiV11Controller 

//    public class ApiV1Controller : Controller
//    {
//        private IApiService _api;
//        private ITasksRepository _repository;
//        private IMappingEngine _mapper;

//        public ApiV1Controller(IApiService auth, ITasksRepository repository, IMappingEngine mapper)
//        {
//            _api = auth;
//            _repository = repository;
//            _mapper = mapper;
//        }

//        [HttpPost]
//        public JsonResult Authenticate(string email, string password)
//        {
//            var success = true;
//            var apiToken = _api.GetApiToken(email, password);

//            if (apiToken == null)
//            {
//                success = false;
//            }

//            return Json(
//                new
//                {
//                    success = success,
//                    data = new { apiToken = apiToken}
//                });
//        }

//        [HttpPost]
//        [TrackyAuthorizeAttribute(LoginController = "Login")]
//        public JsonResult GetAllTasks(string apiToken)
//        {
//            var userId = _api.GetUserIdByApiToken(apiToken);

//            if (userId == 0)
//            {
//                return Json(
//                    new
//                    {
//                        success = false,
//                        data = (string)null
//                    });
//            }


//            var tasksQuery = _repository.Tasks.WithUserId(userId);

//            return Json(
//                new
//                {
//                    success = true,
//                    data = new { tasks = tasksQuery.Select(t => _mapper.Map<Task, TaskDto>(t)).ToList() }
//                });
//        }

//        [HttpPost]
//        [TrackyAuthorizeAttribute(LoginController = "Login")]
//        public JsonResult Submit(string apiToken, IList<TaskDto> tasks)
//        {
//            var newTasks = 0;
//            var updateTasks = 0;

//            var userId = _api.GetUserIdByApiToken(apiToken);

//            if (userId == 0)
//            {
//                return Json(
//                    new
//                    {
//                        success = false,
//                        data = (string)null
//                    });
//            }

//            foreach (var taskData in tasks)
//            {
//                Task task = null;
//                if (taskData.Id == 0)
//                {
//                    newTasks++;
//                    task = new Task();
//                }
//                else
//                {
//                    updateTasks++;
//                    task = _repository.Tasks.WithId(taskData.Id);
//                }

//                task.UserId = userId;
//                task.Number = taskData.Number;
//                task.Description = taskData.Description;
//                task.ActualWork = taskData.ActualWork;

//                _repository.Save(task);
//            }

//            return Json(
//                new
//                {
//                    success = true,
//                    data = new { newTasks = newTasks, updatedTasks = updateTasks, deletedTasks = 0 }
//                });

//        }

//        [HttpPost]
//        [TrackyAuthorizeAttribute(LoginController = "Login")]
//        public JsonResult Delete(string apiToken, IList<TaskDto> tasks)
//        {
//            var deletedTasks = 0;

//            var userId = _api.GetUserIdByApiToken(apiToken);

//            if (userId == 0)
//            {
//                return Json(
//                    new
//                    {
//                        success = false,
//                        data = (string)null
//                    });
//            }

//            foreach (var taskData in tasks)
//            {
//                if (taskData.Id != 0)
//                {
//                    deletedTasks++;

//                    var taskToDelete = _repository.Tasks.WithId(taskData.Id);
//                    _repository.Delete(taskToDelete);
//                }
//            }

//            return Json(
//                new
//                {
//                    success = true,
//                    data = new { newTasks = 0, updatedTasks = 0, deletedTasks = deletedTasks }
//                });
//        }

//    }
//}
