using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Infrastructure.Security
{
    //http://stackoverflow.com/questions/356982/how-to-redirect-to-a-dynamic-login-url-in-asp-net-mvc

    public class TrackyAuthorizeAttribute : AuthorizeAttribute
    {
        public string LoginArea { set; get; }
        public string LoginController { set; get; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (filterContext.Result is HttpUnauthorizedResult)
            {
                if (LoginController != null)
                {
                    filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            { "area", LoginArea ?? "" },
                            { "controller", LoginController },
                            { "ReturnUrl", filterContext.HttpContext.Request.RawUrl }
                        }
                    );
                }
            }
        }
    }
}