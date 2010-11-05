using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trackyourtasks.Core.DAL.Repositories;
using Trackyourtasks.Core.DAL.Extensions;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Controllers
{
    public class AdminUserManagementController : Controller
    {
        private IUsersRepository _usersRepository;

        public AdminUserManagementController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Summary()
        {
            var totalUsersCount = _usersRepository.Users.Count();
            var tempUsersCount = _usersRepository.Users.WithTemp(true).Count();

            return View(new AdminUserSummary(totalUsersCount, tempUsersCount));
        }

        public ActionResult Table()
        {
            return View();
        }

    }
}
