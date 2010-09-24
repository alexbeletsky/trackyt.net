using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trackyourtasks.Core.DAL.DataModel;
using Trackyourtasks.Core.DAL.Repositories;

namespace Web.Areas.Public.Controllers
{
    public class RegistrationController : Controller
    {
        private IUsersRepository _repository;

        //public RegistrationController(IUsersRepository repository)
        //{
        //    _repository = repository;
        //}

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
            //if (ModelState.IsValid)
            //{
            //    var user = new User()
            //    {
            //        Email = model.Email,
            //        Password = model.Password
            //    };
            //}
            return new EmptyResult();
        }
    }
}
