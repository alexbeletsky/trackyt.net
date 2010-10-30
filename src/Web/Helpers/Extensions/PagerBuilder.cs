using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.Mvc;
using System.Security.Policy;

namespace Web.Helpers.Extensions
{
    //Inspired by,
    //http://weblogs.asp.net/gunnarpeipman/archive/2010/02/21/simple-pager-for-asp-net-mvc.aspx?CommentPosted=true#commentmessage
    //

    public class PagerBuilder
    {
        private IList<Page> _pages = new List<Page>();
        private int _totalPages;
        private int _currentPage;
        private int _pagerSize;
        private string _url;

        internal class Page
        {
            private int _index;
            private bool _current;
            private PagerBuilder _builder;

            public Page(PagerBuilder builder, int index, bool current)
            {
                _index = index;
                _current = current;
                _builder = builder;
            }

            public override string ToString()
            {
                var page = new StringBuilder();

                if (_current)
                {
                    page.Append(@"<span class=""current"">" + _index + "</span>");
                }
                else
                {
                    page.Append(@"<span><a href=""" + _builder.PageUrl(_index) + @""">" + _index + "</a></span>");
                }

                return page.ToString();
            }
        }

        internal class PrevControl
        {
            private PagerBuilder _builder;

            public PrevControl(PagerBuilder builder)
            {
                _builder = builder;
            }

            public override string ToString()
            {
                var prevControl = new StringBuilder();

                if (_builder._currentPage == 1)
                {
                    prevControl.Append(@"<div class=""disabled"">« Previous</div>");
                }
                else
                {
                    prevControl.Append(@"<span><a href=""" + _builder.PageUrl(_builder._currentPage - 1) + @""" class=""prev"">« Previous</a></span>");
                }

                return prevControl.ToString();
            }
        }

        internal class NextControl
        {
            private PagerBuilder _builder;

            public NextControl(PagerBuilder builder)
            {
                _builder = builder;

            }

            public override string ToString()
            {
                var nextControl = new StringBuilder();

                if (_builder._currentPage == _builder._totalPages)
                {
                    nextControl.Append(@"<div class=""disabled"">Next »</div>");
                }
                else
                {
                    nextControl.Append(@"<a href=""" + _builder.PageUrl(_builder._currentPage + 1) + @""" class=""next"">Next »</a>");
                }

                return nextControl.ToString();
            }
        }

        public PagerBuilder(string url, int totalPages, int currentPage, int pagerSize)
        {
            _totalPages = totalPages;
            _currentPage = currentPage;
            _pagerSize = pagerSize;
            _url = url;

            for (var index = 1; index < totalPages + 1; index++)
            {
                AddPage(new Page(this, index, index == currentPage));
            }
        }

        public MvcHtmlString ToMvcHtmlString()
        {
            var pager = new StringBuilder();

            pager.Append(@"<div class=""pager"">");

            if (_totalPages > 1)
            {
                AddPrevious(pager);

                var from = Math.Max(_currentPage - _pagerSize/2 - 1, 0);
                var to = Math.Min(_totalPages - 1, _currentPage + _pagerSize/2 - 1);

                for (var page = from; page <= to; page++)
                {
                    pager.Append(_pages[page].ToString());
                }

                AddNext(pager);
            }

            pager.Append(@"</div>");

            return MvcHtmlString.Create(pager.ToString());
        }

        private string PageUrl(int page)
        {
            return String.Format(_url, page);
        }

        private void AddPage(Page page)
        {
            _pages.Add(page);
        }

        private void AddNext(StringBuilder pager)
        {
            var next = new NextControl(this);
            pager.Append(next.ToString());
        }

        private void AddPrevious(StringBuilder pager)
        {
            var previous = new PrevControl(this);
            pager.Append(previous.ToString());
        }
    }
}