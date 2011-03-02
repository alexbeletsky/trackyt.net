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
    //TODO: add tests for untested methods
    [TrackyAuthorizeAttribute(Users = "Admin", LoginArea = "admin", LoginController = "login")]
    public class BlogManagementController : Controller
    {
        private IBlogPostsRepository _blogRepository;

        public BlogManagementController(IBlogPostsRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public ActionResult Index()
        {
            return base.View();
        }

        public ActionResult Summary()
        {
            var blogPostCount = _blogRepository.BlogPosts.Count();

            return base.View(new BlogSummaryModel { TotalPosts = blogPostCount });
        }

        public ActionResult AddPost()
        {
            return base.View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddPost(BlogPost post)
        {
            //TODO: there is a bug here - even blog post could not be save to repository and exception is thrown
            //it is only model state is updated, but post instance is pass as view model. View checks model for null
            //and if it is not null, treat as successful operation. So, in case of failure it will show success message
            //together with error message.
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
            return base.View();
        }

        public JsonResult Posts()
        {
            var allPosts = _blogRepository.BlogPosts;

            return Json(new { success = true, data = new { posts = allPosts.ToArray() } }, JsonRequestBehavior.AllowGet);
        }

        new public ActionResult View(string url)
        {
            var blogPost = _blogRepository.BlogPosts.WithUrl(url);
            
            return base.View(blogPost);
        }

        public ActionResult Edit(string url)
        {
            var blogPost = _blogRepository.BlogPosts.WithUrl(url);
            
            return base.View(blogPost);
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

            return base.View("Updated", blogPost);
        }

        public ActionResult Delete(string url)
        {
            var blogPost = _blogRepository.BlogPosts.WithUrl(url);
            _blogRepository.Delete(blogPost);

            return base.View("Deleted");
        }

        private string CreatePostUrl(string title)
        {
            var titleWithoutPunctuation = new string(title.Where(c => !char.IsPunctuation(c)).ToArray());
            return titleWithoutPunctuation.ToLower().Trim().Replace(" ", "-");
        }

    }
}
