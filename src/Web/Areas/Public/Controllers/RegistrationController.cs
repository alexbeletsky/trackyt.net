using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trackyourtasks.Core.DAL.DataModel;
using Trackyourtasks.Core.DAL.Repositories;
using Trackyourtasks.Core.DAL.Extensions;
using Web.Infrastructure.Security;

namespace Web.Areas.Public.Controllers
{
    //TODO: add functionality for registration of temporary user
    public class RegistrationController : Controller
    {
        private IUsersRepository _repository;
        private IFormsAuthentication _forms;

        public RegistrationController(IUsersRepository repository, IFormsAuthentication forms)
        {
            _repository = repository;
            _forms = forms;
        }

        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Models.RegisterUserModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //check if used already registered
                    if (_repository.Users.WithEmail(model.Email) != null)
                    {
                        ModelState.AddModelError("", "Sorry, user with such email already exist. Please register with different email.");
                    }
                    else
                    {
                        return CreateNewUserAndRedirectToDashboard(model.Email, model.Password);
                    }
                }
                catch (Exception e)
                {
                    return View("Fail", e);
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Fail(Exception exception)
        {
            return View(exception);
        }

        [HttpGet]
        public ActionResult QuickStart()
        {
            try
            {
                var email = GenerateEmail();
                var password = GeneratePassword();

                return CreateNewUserAndRedirectToDashboard(email, password);
            }
            catch (Exception e)
            {
                return View("Fail", e);
            }
        }

        private string GeneratePassword()
        {
            return _forms.GeneratePassword();
        }

        private string GenerateEmail()
        {
            var id = GetLastId();
            var email = "temp" + id + "@tracky.net";
            
            return email;
        }

        private int GetLastId()
        {
            //return new Random(DateTime.Now.Millisecond).Next(10000);
            return _repository.Users.Count();
        }

        private RedirectResult CreateNewUserAndRedirectToDashboard(string email, string password)
        {
            var user = new User
            {
                Email = email,
                Password = password,
                //SecretPhrase = "not-used"
            };

            _repository.SaveUser(user);
            _forms.SetAuthCookie(email, false);

            return Redirect("~/Tracky/Dashboard");
        }
    }
}
