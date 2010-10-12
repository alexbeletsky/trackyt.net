using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Infrastructure.Security;
using Trackyourtasks.Core.DAL.Repositories;
using Trackyourtasks.Core.DAL.Extensions;

namespace Web.Areas.Tracky.Controllers
{
    public class DashboardController : Controller
    {
        private IUsersRepository _repository;
        private IFormsAuthentication _authentication;

        public DashboardController(IUsersRepository repository, IFormsAuthentication authentication)
        {
            _repository = repository;
            _authentication = authentication;
        }

        //TODO: add unit tests
        [Authorize]
        public ActionResult Index()
        {
            var userEmail = _authentication.GetLoggedUser();
            var userId = _repository.GetUsers().WithEmail(userEmail).Id;
            ViewData["UserId"] = userId;
            ViewData["Api"] = VirtualPathUtility.ToAbsolute("~/API/v1");

            return View();
        }

    }
}
