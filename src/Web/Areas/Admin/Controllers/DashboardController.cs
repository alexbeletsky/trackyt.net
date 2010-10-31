using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Infrastructure.Security;

namespace Web.Areas.Admin.Controllers
{
    [TrackyAuthorizeAttribute(Users = "TrackyAdmin", LoginArea = "Admin", LoginController = "AdminLogin") ]
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
