using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Trackyt.Core.DAL.DataModel;
using Web.Helpers;

namespace Trackyt.Core.Tests.Tests.Helpers
{
    [TestFixture]
    public class BlogPostsHelperTests
    {
        [Test]
        public void GetPreviewPost_WithoutPreviewTag_ReturnPostAsOneParagraph()
        {
            // arrange
            var blogPost = new BlogPost
            {
                Url = "post-url",
                Title = "post-title",
                Body = @"<p>First.</p><p>Second.</p>",
                CreatedBy = "alexanderb"
            };

            // act
            var postBody = BlogPostsHelper.GetPreviewPostBody(blogPost);

            // assert
            Assert.That(postBody, Is.EqualTo("First. Second."));
        }

        [Test]
        public void GetPreviewPost_WithPreviewTag_ReturnPreviewTextAsOneParagraph()
        {
            // arrange
            var blogPost = new BlogPost
            {
                Url = "post-url",
                Title = "post-title",
                Body = @"<post-preview><p>First from preview.</p><p>Second from preview.</p></post-preview><p>First.</p><p>Second.</p>",
                CreatedBy = "alexanderb"
            };

            // act
            var postBody = BlogPostsHelper.GetPreviewPostBody(blogPost);

            // assert
            Assert.That(postBody, Is.EqualTo("First from preview. Second from preview. <a href=\"blog/posts/post-url\">Read more...</a>"));
        }

        [Test]
        [ExpectedException(typeof(Exception), ExpectedMessage = "Bad post format: no </post-preview> closing tag.")]
        public void GetPreviewPost_WithPreviewOpenedButNotClosed_ExceptionThrown()
        {
            // arrange
            var blogPost = new BlogPost
            {
                Url = "post-url",
                Title = "post-title",
                Body = @"<post-preview><p>First from preview.</p><p>Second from preview.</p><post-preview><p>First.</p><p>Second.</p>",
                CreatedBy = "alexanderb"
            };

            // act
            var postBody = BlogPostsHelper.GetPreviewPostBody(blogPost);

            // assert
        }

        [Test]
        public void GetPostPreview_WithPreviewTag_ReadMoreHrefAdded()
        {
            // arrange
            var blogPost = new BlogPost
            {
                Url = "post-url",
                Title = "post-title",
                Body = @"<post-preview><p>First from preview.</p><p>Second from preview.</p></post-preview><p>First.</p><p>Second.</p>",
                CreatedBy = "alexanderb"
            };

            // act
            var postBody = BlogPostsHelper.GetPreviewPostBody(blogPost);

            // assert
            Assert.That(postBody, Is.EqualTo("First from preview. Second from preview. <a href=\"blog/posts/post-url\">Read more...</a>"));
        }

        [Test]
        public void GetPost_WithoutPreviewTag_ReturnsUnmodifiedPost()
        {
            // arrange
            var blogPost = new BlogPost
            {
                Url = "post-url",
                Title = "post-title",
                Body = @"<p>First.</p><p>Second.</p>",
                CreatedBy = "alexanderb"
            };

            // act
            var postBody = BlogPostsHelper.GetPostBody(blogPost);

            // assert
            Assert.That(postBody, Is.EqualTo("<p>First.</p><p>Second.</p>"));
        }
    }
}
