using System.Web.Mvc;
using Web.Models;
using System.Linq;
using Trackyt.Core.DAL.Repositories;
using Web.Helpers;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private IBlogPostsRepository _posts;

        public HomeController(IBlogPostsRepository posts)
        {
            _posts = posts;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Faq()
        {
            return View();
        }

        public ActionResult Api()
        {
            return View();
        }

        public ActionResult ApiV11()
        {
            return View();
        }

        public ActionResult ApiV10()
        {
            return View();
        }

        public ActionResult LastArticleFromBlog()
        {
            var lastPostFromBlog = _posts.BlogPosts.FirstOrDefault();
            if (lastPostFromBlog == null)
            {
                return View();
            }

            var model = new LastArticleFromBlogModel 
            { 
                Title = lastPostFromBlog.Title, HtmlBody = BlogPostsHelper.GetPreviewPostBody(lastPostFromBlog) 
            };

            return View(model);
        }
    }
}
