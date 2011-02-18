using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Web.Areas.Admin.Controllers;
using System.Web.Mvc;
using Web.Areas.Admin.Models;
using Moq;
using Trackyt.Core.DAL.Repositories;
using Trackyt.Core.DAL.DataModel;

namespace Trackyt.Core.Tests.Tests.Controllers.Admin
{
    [TestFixture]
    public class BlogManagementControllerTests
    {
        [Test]
        public void Smoke()
        {
            //arrange
            var blogRepository = new Mock<IBlogPostsRepository>();
            var blogManagement = new BlogManagementController(blogRepository.Object);

            //act/post
            Assert.That(blogManagement, Is.Not.Null);
        }

        [Test]
        public void Index_Get_ReturnsView()
        {
            //arrange
            var blogRepository = new Mock<IBlogPostsRepository>();
            var blogManagement = new BlogManagementController(blogRepository.Object);

            //act
            var result = blogManagement.Index() as ViewResult;

            //post
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Summary_Get_ReturnsSummaryObject()
        {
            //arrange
            var blogRepository = new Mock<IBlogPostsRepository>();
            var blogPosts = new List<BlogPost>
            {
                new BlogPost { Url = "xx", Body = "xx post", CreatedBy = "alexanderb", CreatedDate=DateTime.Now, Title="xx title" },
                new BlogPost { Url = "xxx", Body = "xx post", CreatedBy = "alexanderb", CreatedDate=DateTime.Now, Title="xx title" },
                new BlogPost { Url = "xxxx", Body = "xx post", CreatedBy = "alexanderb", CreatedDate=DateTime.Now, Title="xx title" },
                new BlogPost { Url = "xxxxx", Body = "xx post", CreatedBy = "alexanderb", CreatedDate=DateTime.Now, Title="xx title" },
            };

            blogRepository.Setup(b => b.BlogPosts).Returns(blogPosts.AsQueryable());

            var blogManagement = new BlogManagementController(blogRepository.Object);

            //act
            var result = blogManagement.Summary() as ViewResult;

            //post
            var model = result.ViewData.Model as BlogSummaryModel;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.TotalPosts, Is.EqualTo(4));
        }

        [Test]
        public void AddPost_Get_ReturnsView()
        {
            //arrange
            var blogRepository = new Mock<IBlogPostsRepository>();
            var blogManagement = new BlogManagementController(blogRepository.Object);

            //act
            var result = blogManagement.AddPost() as ViewResult;

            //post
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void AddPost_Post_UrlForPost_Is_Created()
        {
            //arrange
            var blogRepository = new Mock<IBlogPostsRepository>();
            var submittedPosts = new List<BlogPost>();
            blogRepository.Setup(b => b.Save(It.IsAny<BlogPost>())).Callback((BlogPost p) => submittedPosts.Add(p));

            var blogManagement = new BlogManagementController(blogRepository.Object);
            var model = new BlogPost { Title = "Hey Joe" };
            
            //act
            var result = blogManagement.AddPost(model) as ViewResult;

            //post
            var post = submittedPosts.First();
            Assert.That(model.Url, Is.EqualTo("hey-joe"));
        }

        [Test]
        public void AddPost_Post_NotAddPost_With_SameUrl()
        {
            //arrange
            var blogRepository = new Mock<IBlogPostsRepository>();
            var submittedPosts = new List<BlogPost>();
            blogRepository.Setup(b => b.Save(It.IsAny<BlogPost>()))
                .Callback((BlogPost p) => 
                {
                    if (submittedPosts.Find(x => x.Url == p.Url) != null)
                    {
                        throw new Exception();
                    }
                    submittedPosts.Add(p); 
                }
                );

            var blogManagement = new BlogManagementController(blogRepository.Object);
            var model = new BlogPost { Title = "Hey Joe" };
            
            //act
            var result = blogManagement.AddPost(model) as ViewResult;
            result = blogManagement.AddPost(model) as ViewResult;

            //post
            Assert.That(blogManagement.ModelState[""].Errors[0].ErrorMessage, Is.EqualTo("Post could not be added. Exception of type 'System.Exception' was thrown."));
        }

        [Test]
        public void AddPost_Post_CreatedDate_Contains_HourMinutesSeconds()
        {
            //arrange
            var blogRepository = new Mock<IBlogPostsRepository>();
            var submittedPosts = new List<BlogPost>();
            blogRepository.Setup(b => b.Save(It.IsAny<BlogPost>()))
                .Callback((BlogPost p) =>
                {
                    if (submittedPosts.Find(x => x.Url == p.Url) != null)
                    {
                        throw new Exception();
                    }
                    submittedPosts.Add(p);
                }
                );

            var blogManagement = new BlogManagementController(blogRepository.Object);
            var model = new BlogPost { Title = "Hey Joe" };

            //act
            var result = blogManagement.AddPost(model) as ViewResult;

            //post
            var post = submittedPosts.First();
            Assert.That(post.CreatedDate, Is.Not.Null);
            Assert.That(post.CreatedDate.ToLongTimeString(), Is.Not.EqualTo("00:00:00"));
        }

        [Test]
        public void AddPost_Post_Url_Contains_No_Punctuation()
        {
            //arrange
            var blogRepository = new Mock<IBlogPostsRepository>();
            var submittedPosts = new List<BlogPost>();
            blogRepository.Setup(b => b.Save(It.IsAny<BlogPost>()))
                .Callback((BlogPost p) =>
                {
                    if (submittedPosts.Find(x => x.Url == p.Url) != null)
                    {
                        throw new Exception();
                    }
                    submittedPosts.Add(p);
                }
                );

            var blogManagement = new BlogManagementController(blogRepository.Object);
            var model = new BlogPost { Title = "Hey, Joe.!:;" };

            //act
            var result = blogManagement.AddPost(model) as ViewResult;

            //post
            var post = submittedPosts.First();
            Assert.That(model.Url, Is.EqualTo("hey-joe"));
        }
    }
}
