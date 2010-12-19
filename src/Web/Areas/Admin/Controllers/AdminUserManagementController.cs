﻿using System.Linq;
using System.Web.Mvc;
using Trackyt.Core.DAL.Extensions;
using Trackyt.Core.DAL.Repositories;
using Web.Areas.Admin.Models;
using Web.Infrastructure.Security;

namespace Web.Areas.Admin.Controllers
{
    [TrackyAuthorizeAttribute(Users = "Admin", LoginArea = "Admin", LoginController = "AdminLogin")]
    public class AdminUserManagementController : Controller
    {
        private IUsersRepository _users;

        public AdminUserManagementController(IUsersRepository usersRepository)
        {
            _users = usersRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Summary()
        {
            var totalUsersCount = _users.Users.Count();
            var tempUsersCount = _users.Users.WithTemp(true).Count();

            return View(new AdminUserSummary(totalUsersCount, tempUsersCount));
        }

        public ActionResult Table()
        {
            return View(_users.Users.ToList());
        }
    }
}
