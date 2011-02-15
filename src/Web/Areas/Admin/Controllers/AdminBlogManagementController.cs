using System;
using System.Linq;
using System.Web.Mvc;
using Trackyt.Core.DAL.DataModel;
using Trackyt.Core.DAL.Repositories;
using Web.Areas.Admin.Models;
using Web.Infrastructure.Security;
using Trackyt.Core.DAL.Extensions;

namespace Web.Areas.Admin.Controllers
{
    [TrackyAuthorizeAttribute(Users = "Admin", LoginArea = "Admin", LoginController = "AdminLogin")]
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
                    post.Url = CreatePostUrl(post.Title);                    
                    post.CreatedDate = DateTime.Now; 

                    _blogRepository.Save(post);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Post could not be added. " + e.Message);
            }

            return View(post);
        }

        public ActionResult AllPosts()
        {
            return View();
        }

        public JsonResult Posts()
        {
            return Json(new { success = true, data = new { posts = _blogRepository.BlogPosts.ToArray() } }, JsonRequestBehavior.AllowGet);
        }

        new public ActionResult View(string url)
        {
            var blogPost = _blogRepository.BlogPosts.WithUrl(url);
            return View(blogPost);
        }

        public ActionResult Edit(string url)
        {
            var blogPost = _blogRepository.BlogPosts.WithUrl(url);
            return View(blogPost);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(BlogPost model)
        {
            var blogPost = _blogRepository.BlogPosts.WithUrl(model.Url);

            blogPost.Title = model.Title;
            blogPost.Body = model.Body;
            blogPost.CreatedBy = model.CreatedBy;

            _blogRepository.Save(blogPost);

            return View("Updated", blogPost);
        }

        private string CreatePostUrl(string title)
        {
            var titleWithoutPunctuation = new string(title.Where(c => !char.IsPunctuation(c)).ToArray());
            return titleWithoutPunctuation.ToLower().Trim().Replace(" ", "-");
        }

    }
}
