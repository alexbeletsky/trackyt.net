using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trackyourtasks.Core.DAL.Repositories;
using Web.Areas.Public.Models;
using Trackyourtasks.Core.DAL.Extensions;
using System.Web.Security;
using Web.Infrastructure.Security;

namespace Web.Areas.Public.Controllers
{
    public class LoginController : Controller
    {
        private IUsersRepository _repository;
        private IFormsAuthentication _authentication;

        public LoginController(IUsersRepository repository, IFormsAuthentication auth)
        {
            _repository = repository;
            _authentication = auth;
        }


        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if(ModelState.IsValid) 
            {
                if (_repository.GetUsers().WithEmail(model.Email) != null)
                {
                    _authentication.SetAuthCookie(model.Email, false);
                    return Redirect(returnUrl ?? "/Tracky/Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            return View("Index", model);
        }

    }
}
