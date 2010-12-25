using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trackyt.Core.Services;
using Trackyt.Core.DAL.Repositories;
using AutoMapper;

namespace Web.API.v11.Controllers
{
    public class ApiV11Controller : Controller
    {
        private IApiService _api;
        private ITasksRepository _repository;
        private IMappingEngine _mapper;

        public ApiV11Controller(IApiService auth, ITasksRepository repository, IMappingEngine mapper)
        {
            _api = auth;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public JsonResult Authenticate(string email, string password)
        {
            var success = true;
            var apiToken = _api.GetApiToken(email, password);

            if (apiToken == null)
            {
                success = false;
            }

            return Json(
                new
                {
                    success = success,
                    data = new { apiToken = apiToken }
                });
        }
    }
}
