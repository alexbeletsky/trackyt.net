using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trackyourtasks.Core.DAL.DataModel;

namespace Web.Areas.Public.Controllers
{
    public class RegistrationController : Controller
    {
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
