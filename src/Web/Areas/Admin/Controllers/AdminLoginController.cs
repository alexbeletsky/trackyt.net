using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Areas.Admin.Models;
using Web.Infrastructure.Security;

namespace Web.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        private static readonly string AdminPassword = "trk$adm9cls!22";
        private IFormsAuthentication _authentication;

        public AdminLoginController(IFormsAuthentication auth)
        {
            _authentication = auth;
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login(AdminLogin model)
        {
            if (ModelState.IsValid)
            {
                var password = model.Password;
                if (password == AdminPassword)
                {
                    _authentication.SetAuthCookie("TrackyAdmin", false);
                    return Redirect("~/Admin/Dashboard");
                }   
                else
                {
                    return Redirect("~/Admin");
                }
            }

            return View();
        }
    }
}
