using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trackyourtasks.Core.DAL.Repositories;
using Web.Areas.Admin.Models;
using Trackyourtasks.Core.DAL.DataModel;
using Web.Infrastructure.Security;

namespace Web.Areas.Admin.Controllers
{
    [TrackyAuthorizeAttribute(Users = "TrackyAdmin", LoginArea = "Admin", LoginController = "AdminLogin")]
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
            //var dummy = new BlogPost { CreatedDate = DateTime.Now };

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddPost(BlogPost post)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    post.Url = post.Title.ToLower().Trim().Replace(" ", "-");                    
                    post.CreatedDate = DateTime.Now; 

                    _blogRepository.SaveBlogPost(post);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Post could not be added. " + e.Message);
            }

            return View(post);
        }

    }
}
