using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyt.Core.DAL.DataModel;

namespace Trackyt.Core.DAL.Extensions
{
    public static class BlogPostsExtensions
    {
        public static BlogPost WithId(this IQueryable<BlogPost> posts, int id)
        {
            return posts.Where(p => p.Id == id).SingleOrDefault();
        }

        public static BlogPost WithUrl(this IQueryable<BlogPost> posts, string url)
        {
            return posts.Where(p => p.Url == url).SingleOrDefault();
        }

        public static IQueryable<BlogPost> Page(this IQueryable<BlogPost> posts, int page, int pageSize)
        {
            return posts.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
