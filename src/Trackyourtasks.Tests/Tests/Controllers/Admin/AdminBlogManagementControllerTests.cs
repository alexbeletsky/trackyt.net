using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Web.Areas.Admin.Controllers;
using System.Web.Mvc;
using Web.Areas.Admin.Models;
using Moq;
using Trackyourtasks.Core.DAL.Repositories;
using Trackyourtasks.Core.DAL.DataModel;

namespace Trackyourtasks.Core.Tests.Tests.Controllers.Admin
{
    [TestFixture]
    public class AdminBlogManagementControllerTests
    {
        [Test]
        public void Smoke()
        {
            //arrange
            var blogRepository = new Mock<IBlogPostsRepository>();
            var blogManagement = new AdminBlogManagementController(blogRepository.Object);

            //act/post
            Assert.That(blogManagement, Is.Not.Null);
        }

        [Test]
        public void Index_Get_ReturnsView()
        {
            //arrange
            var blogRepository = new Mock<IBlogPostsRepository>();
            var blogManagement = new AdminBlogManagementController(blogRepository.Object);

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

            var blogManagement = new AdminBlogManagementController(blogRepository.Object);

            //act
            var result = blogManagement.Summary() as ViewResult;

            //post
            var model = result.ViewData.Model as AdminBlogSummary;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.TotalPosts, Is.EqualTo(4));
        }

        [Test]
        public void AddPost_Get_ReturnsView()
        {
            //arrange
            var blogRepository = new Mock<IBlogPostsRepository>();
            var blogManagement = new AdminBlogManagementController(blogRepository.Object);

            //act
            var result = blogManagement.AddPost() as ViewResult;

            //post
            Assert.That(result, Is.Not.Null);
        }
    }
}
