using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Trackyourtasks.Core.Tests.Framework;
using Trackyourtasks.Core.DAL.Repositories.Impl;
using Trackyourtasks.Core.DAL.DataModel;
using Trackyourtasks.Core.DAL.Extensions;

namespace Trackyourtasks.Database.Tests.Tests.DAL
{
    [TestFixture]
    public class BlogPostsRepositoryTests
    {
        [Test]
        public void Smoke()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //arrange
                var repository = new BlogPostsRepository();

                //act/post
                Assert.That(repository, Is.Not.Null);
            }
        }

        [Test]
        public void SaveBlogPost()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //arrange
                var repository = new BlogPostsRepository(fixture.Setup.Context);
                var post = new BlogPost
                {
                    Url = "Some-new-post",
                    Title = "Some new post",
                    Body = "<p>This is new post in blog</p>",
                    CreatedDate = DateTime.Now
                };

                //act
                repository.SaveBlogPost(post);

                //assert
                Assert.That(post.Id, Is.GreaterThan(0));
            }
        }

        [Test]
        public void DeletePost()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //arrange
                var repository = new BlogPostsRepository(fixture.Setup.Context);
                var post = new BlogPost
                {
                    Url = "Some-new-post",
                    Title = "Some new post",
                    Body = "<p>This is new post in blog</p>",
                    CreatedDate = DateTime.Now
                };

                repository.SaveBlogPost(post);

                //act
                repository.DeleteBlogPost(post);

                //assert
                var foundTask = repository.BlogPosts.WithId(post.Id);
                Assert.That(foundTask, Is.Null);
            }
        }
    }
}
