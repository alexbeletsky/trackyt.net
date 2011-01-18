using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace Web.Helpers.Extensions
{
    public static class PagerHtmlExtension
    {
        /// <summary>
        /// Generates HTML for blog pager
        /// </summary>
        /// <param name="helper">HtmlHelper instance</param>
        /// <param name="totalPages">Total pages of blog</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="pagerSize">Pager size (default is 5)</param>
        /// <returns></returns>
        public static MvcHtmlString Pager(this HtmlHelper helper, string url, int totalPages, int currentPage, int pagerSize = 5)
        {
            var pageBuilder = new PagerBuilder(HttpUtility.UrlDecode(url), totalPages, currentPage, pagerSize);

            return pageBuilder.ToMvcHtmlString();
        }
    }
}