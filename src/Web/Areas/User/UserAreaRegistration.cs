using System.Web.Mvc;

namespace Web.Areas.User
{
    [CoverageExcludeAttribute]
    public class UserAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "user";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "User",
                "user/{controller}/{action}/{id}",
                new { action = "Index", controller = "Dashboard", id = UrlParameter.Optional },
                new string[] { "Web.Areas.User.Controllers" }
            );
        }
    }
}
