using System.Linq;
using System.Web.Mvc;
using Trackyt.Core.DAL.DataModel;
using Trackyt.Core.DAL.Extensions;
using Trackyt.Core.DAL.Repositories;
using Trackyt.Core.Security;
using Trackyt.Core.Services;
using System;

namespace Web.Controllers
{
    public class RegistrationController : Controller
    {
        private IAuthenticationService _auth;

        public RegistrationController(IAuthenticationService auth)
        {
            _auth = auth;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Models.RegisterUserModel model)
        {
            if (ModelState.IsValid)
            {
                //check if used already registered
                if (_auth.CreateUser(model.Email, model.Password, false))
                {
                    return Redirect("~/User/Dashboard");
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

            _auth.CreateUser(email, password, true);

            return Redirect("~/User/Dashboard");
        }
    }
}
