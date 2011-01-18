using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trackyt.Core.DAL.DataModel;

namespace Web.Helpers
{
    public static class BlogPostsHelper
    {
        public static string GetPreviewPostBody(BlogPost post)
        {
            var processor = new BlogPostProcessor(post);

            return processor.GetPreviewPostBody();
        }

        public static string GetPostBody(BlogPost post)
        {
            var processor = new BlogPostProcessor(post);

            return processor.GetPostBody();
        }
    }
}