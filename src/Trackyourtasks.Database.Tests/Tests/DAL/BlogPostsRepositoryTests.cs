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

        #region test data
        
        private static void SubmitTenBlogpostsToRepository(BlogPostsRepository repository)
        {
            for (var blog = 0; blog < 10; blog++)
            {
                var post = new BlogPost
                {
                    Url = "Url-" + blog,
                    Title = "Some new post: " + blog,
                    Body = "<p>This is new post in blog</p>",
                    CreatedBy = "AlexanderB",
                    CreatedDate = (DateTime.Now.AddDays(blog))
                };

                repository.SaveBlogPost(post);
            }
        }

        #endregion

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
                    CreatedBy = "AlexanderB",
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
                    CreatedBy = "AlexanderB",
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

        [Test]
        public void PagingGetFirstPage()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //arrange
                var repository = new BlogPostsRepository(fixture.Setup.Context);

                SubmitTenBlogpostsToRepository(repository);

                //act
                var page = repository.BlogPosts.Page(1, 5);

                //assert
                Assert.That(page, Is.Not.Null);
                Assert.That(page.Count(), Is.EqualTo(5));
                Assert.That(page.First().Url, Is.EqualTo("Url-9"));
            }
        }

        [Test]
        public void PagingGetSecondPage()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //arrange
                var repository = new BlogPostsRepository(fixture.Setup.Context);

                SubmitTenBlogpostsToRepository(repository);

                //act
                var page = repository.BlogPosts.Page(2, 5);

                //assert
                Assert.That(page, Is.Not.Null);
                Assert.That(page.Count(), Is.EqualTo(5));
                Assert.That(page.First().Url, Is.EqualTo("Url-4"));
            }
        }

        [Test]
        public void PagingGetNonExistingPage()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //arrange
                var repository = new BlogPostsRepository(fixture.Setup.Context);

                SubmitTenBlogpostsToRepository(repository);

                //act
                var page = repository.BlogPosts.Page(3, 5);

                //assert
                Assert.That(page, Is.Not.Null);
                Assert.That(page.Count(), Is.EqualTo(0));
            }
        }

        [Test]
        public void BlogPostsSortedByDate()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //arrange
                var repository = new BlogPostsRepository(fixture.Setup.Context);

                SubmitTenBlogpostsToRepository(repository);

                //act
                var posts = repository.BlogPosts.ToArray();

                //post
                Assert.That(posts, Is.Not.Null);
                Assert.That(posts[0].CreatedDate, Is.GreaterThan(posts[1].CreatedDate));
                Assert.That(posts[1].CreatedDate, Is.GreaterThan(posts[2].CreatedDate));
                Assert.That(posts[2].CreatedDate, Is.GreaterThan(posts[3].CreatedDate));
            }

        }
    }
}
