using System.Web.Mvc;
using Trackyt.Core.Services;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private IAuthenticationService _auth;

        public LoginController(IAuthenticationService auth)
        {
            _auth = auth;
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login(AdminLogin model)
        {
            if (ModelState.IsValid)
            {
                if (_auth.Authenticate("Admin", model.Password))
                {
                    return Redirect("~/admin/dashboard");
                }   
                else
                {
                    return Redirect("~/admin");
                }
            }

            return View();
        }
    }
}
