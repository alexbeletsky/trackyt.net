using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Infrastructure.Security;
using Trackyourtasks.Core.DAL.Repositories;
using Trackyourtasks.Core.DAL.Extensions;
using Web.Infrastructure.Helpers;

namespace Web.Areas.Tracky.Controllers
{
    public class DashboardController : Controller
    {
        private IUsersRepository _repository;
        private IFormsAuthentication _authentication;
        private IPathHelper _path;

        public DashboardController(IUsersRepository repository, IFormsAuthentication authentication, IPathHelper path)
        {
            _repository = repository;
            _authentication = authentication;
            _path = path;
        }

        [Authorize]
        public ActionResult Index()
        {
            var userEmail = _authentication.GetLoggedUser();
            var userId = _repository.GetUsers().WithEmail(userEmail).Id;
            ViewData["UserId"] = userId;
            ViewData["Api"] = _path.VirtualToAbsolute("~/API/v1");

            return View();
        }

    }
}
