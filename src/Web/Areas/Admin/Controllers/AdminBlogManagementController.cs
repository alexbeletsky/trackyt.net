using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trackyourtasks.Core.DAL.Repositories;
using Web.Areas.Admin.Models;
using Trackyourtasks.Core.DAL.DataModel;

namespace Web.Areas.Admin.Controllers
{
    public class AdminBlogManagementController : Controller
    {
        private IBlogPostsRepository _blogRepository;

        public AdminBlogManagementController(IBlogPostsRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Summary()
        {
            var blogPostCount = _blogRepository.BlogPosts.Count();

            return View(new AdminBlogSummary(blogPostCount));
        }

        public ActionResult AddPost()
        {
            var dummy = new BlogPost { CreatedDate = DateTime.Now };

            return View(dummy);
        }

        [HttpPost]
        public ActionResult AddPost(BlogPost post)
        {
            post.Url = post.Title.Replace(" ", "").Trim();
            _blogRepository.SaveBlogPost(post);

            return View("Posted");
        }

    }
}
