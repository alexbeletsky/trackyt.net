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
        private IRedirectService _redirect;

        public RegistrationController(IAuthenticationService auth, INotificationService notification, IRedirectService redirect)
        {
            _auth = auth;
            _notification = notification;
            _redirect = redirect;
        }

        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult QuickStart()
        {
            var postfix = DateTime.Now.ToFileTimeUtc().ToString().Substring(12);
            var email = "temp" + postfix + "@trackyt.net";
            var password = email;

            _auth.RegisterTemporaryUser(email, password);

            return _redirect.ToDashboard(email);
        }

        [HttpGet]
        public ActionResult Mobile()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Success()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterUserModel model)
        {
            return RegiterWithAction(model, () => { return _redirect.ToDashboard(model.Email); }, () => { return View("index", model); });
        }

        [HttpPost]
        public ActionResult RegisterMobile(RegisterUserModel model)
        {
            return RegiterWithAction(model, () => { return _redirect.ToRegistrationSuccess(); }, () => { return View("mobile", model); });
        }

        private ActionResult RegiterWithAction(RegisterUserModel model, Func<ActionResult> successAction, Func<ActionResult> defaultAction)
        {
            if (ModelState.IsValid)
            {
                var email = model.Email;
                var password = model.Password;

                if (_auth.RegisterNewUser(email, password))
                {
                    _notification.NotifyUserOnRegistration(email, password);
                    return successAction.Invoke();
                }
                else
                {
                    ModelState.AddModelError("", "Sorry, user with such email already exist. Please register with different email.");
                }
            }

            return defaultAction.Invoke();
        }

    }
}
