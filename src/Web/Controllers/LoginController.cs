using System.Web.Mvc;
using Trackyt.Core.Services;
using Web.Models;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        private IAuthenticationService _auth;
        private IRedirectService _redirect;

        public LoginController(IAuthenticationService authentication, IRedirectService redirect)
        {
            _auth = authentication;
            _redirect = redirect; 
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
                if (_auth.Authenticate(model.Email, model.Password))
                {
                    return returnUrl == null ? _redirect.ToDashboard(model.Email) : _redirect.ToUrl(returnUrl);
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
