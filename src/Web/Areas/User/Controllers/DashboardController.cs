using System.Web.Mvc;
using Trackyt.Core.DAL.Extensions;
using Trackyt.Core.DAL.Repositories;
using Trackyt.Core.Security;
using Web.Infrastructure.Security;
using Trackyt.Core.Services;

namespace Web.Areas.User.Controllers
{
    [TrackyAuthorizeAttribute(LoginController = "Login")]
    public class DashboardController : Controller
    {
        private IUsersRepository _users;
        private IPathHelper _path;

        public DashboardController(IUsersRepository repository, IPathHelper path)
        {
            _users = repository;
            _path = path;
        }

        public ActionResult Index(string email)
        {
            var user = _users.Users.WithEmail(email);
            
            ViewData["Api"] = _path.VirtualToAbsolute("~/API/v1.1/");
            ViewData["Email"] = email;
            ViewData["ApiToken"] = user.ApiToken;

            return View();
        }

    }
}
