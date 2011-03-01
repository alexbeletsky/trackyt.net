using System.Linq;
using System.Web.Mvc;
using Trackyt.Core.DAL.DataModel;
using Trackyt.Core.DAL.Extensions;
using Trackyt.Core.DAL.Repositories;
using Trackyt.Core.Security;
using Trackyt.Core.Services;
using System;
using Web.Models;

namespace Web.Controllers
{
    public class RegistrationController : Controller
    {
        private IAuthenticationService _auth;
        private INotificationService _notification;

        public RegistrationController(IAuthenticationService auth, INotificationService notification)
        {
            _auth = auth;
            _notification = notification;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterUserModel model)
        {
            if (ModelState.IsValid)
            {
                var email = model.Email;
                var password = model.Password;

                if (_auth.RegisterNewUser(email, password))
                {
                    _notification.NotifyUserOnRegistration(email, password);
                    return Redirect("~/user/dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Sorry, user with such email already exist. Please register with different email.");
                }
            }

            return View("Index", model);
        }

        [HttpGet]
        public ActionResult QuickStart()
        {
            var postfix = DateTime.Now.ToFileTimeUtc().ToString().Substring(12);
            var email = "temp" + postfix + "@trackyt.net";
            var password = email;

            _auth.RegisterTemporaryUser(email, password);

            return Redirect("~/user/dashboard");
        }
    }
}
