using System.Web.Mvc;
using Web.Infrastructure.Security;
using System.Web;

namespace Web.Areas.Admin.Controllers
{
    class ElmahResult : ActionResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            var factory = new Elmah.ErrorLogPageFactory();
            var handler = factory.GetHandler(HttpContext.Current, "html", "", "");

            handler.ProcessRequest(HttpContext.Current);
        }
    }

    [TrackyAuthorizeAttribute(Users = "Admin", LoginArea = "Admin", LoginController = "AdminLogin")]
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Elmah()
        {
            return new ElmahResult();
        }

    }
}
