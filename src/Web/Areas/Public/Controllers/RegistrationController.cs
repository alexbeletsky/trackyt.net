using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trackyourtasks.Core.DAL.DataModel;
using Trackyourtasks.Core.DAL.Repositories;
using Trackyourtasks.Core.DAL.Extensions;

namespace Web.Areas.Public.Controllers
{
    //TODO: add functionality for registration of temporary user
    public class RegistrationController : Controller
    {
        private IUsersRepository _repository;

        public RegistrationController(IUsersRepository repository)
        {
            _repository = repository;
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
                    if (_repository.GetUsers().WithEmail(model.Email) != null)
                    {
                        ModelState.AddModelError("", "Sorry, user with such email already exist. Please register with different email.");
                    }
                    else
                    {
                        var user = new User()
                        {
                            Email = model.Email,
                            Password = model.Password,
                            SecretPhrase = "not-used"
                        };

                        _repository.SaveUser(user);
                        return Redirect("/Tracky/Dashboard");
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
    }
}
