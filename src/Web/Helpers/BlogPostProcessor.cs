using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trackyt.Core.DAL.DataModel;

namespace Web.Helpers
{
    class BlogPostProcessor
    {
        private BlogPost _post;

        private readonly string PostPreviewOpenTag = "<post-preview>";
        private readonly string PostPreviewCloseTag = "</post-preview>";
        private readonly string ParagraphOpenTag = "<p>";
        private readonly string ParagraphCloseTag = "</p>";
        private readonly string ReadMoreHrefFormat = @"<a href=""{0}"">Read more...</a>";
        private readonly string Space = " ";

        public BlogPostProcessor(BlogPost post)
        {
            _post = post;
        }

        public string GetPreviewPostBody()
        {
            var body = _post.Body;
            var addReadMoreHref = false;

            if (CheckOpenTag(body) && CheckCloseTag(body))
            {
                var start = body.IndexOf(PostPreviewOpenTag) + PostPreviewOpenTag.Length;
                var lenght = body.IndexOf(PostPreviewCloseTag) - start;
                body = body.Substring(start, lenght);
                addReadMoreHref = true;
            }

            body = body.Replace(ParagraphOpenTag, string.Empty);
            body = body.Replace(ParagraphCloseTag, Space);

            if (addReadMoreHref)
            {
                body += string.Format(ReadMoreHrefFormat, "blog/posts/" + _post.Url);
            }

            if (body.EndsWith(Space))
            {
                body = body.Remove(body.Length - 1);
            }

            return body;
        }

        public string GetPostBody()
        {
            var body = _post.Body;

            body = body.Replace(PostPreviewOpenTag, string.Empty);
            body = body.Replace(PostPreviewCloseTag, string.Empty);

            return body;
        }

        private bool CheckCloseTag(string body)
        {
            var result = body.Contains(PostPreviewCloseTag);

            if (!result)
            {
                throw new Exception("Bad post format: no </post-preview> closing tag.");
            }

            return result;
        }

        private bool CheckOpenTag(string body)
        {
            return body.Contains(PostPreviewOpenTag);
        }
    }

}