using System.Web.Mvc;

namespace Web.Areas.Blog
{
    public class BlogAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Blog";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Blog_RssFeed",
                "Blog/feed.xml",
                new { controller = "Rss", action = "Feed" },
                new string[] { "Web.Areas.Blog.Controllers" }
            );

            context.MapRoute(
                "Blog_PostByUrl",
                "Blog/Posts/{url}",
                new { controller = "Posts", action = "PostByUrl" },
                new string[] { "Web.Areas.Blog.Controllers" }
            );

            context.MapRoute(
                "Blog_default",
                "Blog/{controller}/{action}/{id}",
                new { action = "Index", controller = "Posts", id = UrlParameter.Optional },
                new string[] { "Web.Areas.Blog.Controllers" }
            );

        }
    }
}
