using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trackyt.Core.Services;
using Trackyt.Core.DAL.Repositories;
using Trackyt.Core.DAL.Extensions;

namespace Web.Areas.User.Controllers
{
    public class ShareController : Controller
    {
        private IShareService _shareService;
        private ITasksRepository _tasksRepository;
        private IUsersRepository _usersRepository;

        public ShareController(IShareService shareService, ITasksRepository tasks, IUsersRepository users)
        {
            _shareService = shareService;
            _tasksRepository = tasks;
            _usersRepository = users;
        }
        
        public ActionResult Index(string email, string key)
        {
            if (string.IsNullOrEmpty(key) || !_shareService.ValidateShareKey(email, key))
            {
                return RedirectToAction("error");
            }

            ViewBag.Email = email;

            var user = _usersRepository.Users.WithEmail(email);
            var tasks = _tasksRepository.Tasks.WithUserId(user.Id).ToList();

            return View(tasks);
        }

        public ActionResult Error()
        {
            return View();
        }

    }
}
