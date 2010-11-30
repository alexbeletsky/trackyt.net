using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Areas.Admin.Models;
using Web.Infrastructure.Security;
using Trackyt.Core.Services;

namespace Web.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        private IAuthenticationService _authentication;

        public AdminLoginController(IAuthenticationService auth)
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
                if (_authentication.Authenticate("Admin", model.Password))
                {
                    return Redirect("~/Admin/AdminDashboard");
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
