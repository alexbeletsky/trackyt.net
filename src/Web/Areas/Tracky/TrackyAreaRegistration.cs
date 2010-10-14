using System.Web.Mvc;

namespace Web.Areas.Tracky
{
    [CoverageExcludeAttribute]
    public class TrackyAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Tracky";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Tracky_default",
                "Tracky/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
