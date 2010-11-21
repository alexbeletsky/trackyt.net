using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Web.Helpers.Extensions;
using System.Web.Mvc;

namespace Trackyt.Core.Tests.Tests.Helpers
{
    /// <summary>
    /// HTML generation tests for PagerHtmlExtention
    /// </summary>
    [TestFixture]
    public class PagerHtmlExtensionTests
    {
        [Test]
        public void Smoke()
        {
            //arrange/act
            var pager = PagerHtmlExtension.Pager(null, "/Posts/Page/{0}", 1, 0);

            //assert
            Assert.That(pager, Is.Not.Null);
        }

        [Test]
        public void CreateOnePagePager_FirstIsCurrent_PagerIsEmpty()
        {
            //arrange/act
            var pager = PagerHtmlExtension.Pager(null, "/Posts/Page/{0}", 1, 0);

            //assert
            var expected = @"<div class=""pager""></div>";
            Assert.That(pager.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void CreateTwoPagePages_FirstIsCurrent_PagerWithTwoPages()
        {
            //arrange/act
            var pager = PagerHtmlExtension.Pager(null, "/Posts/Page/{0}", 2, 1);
            
            //assert
            var expected = @"<div class=""pager""><div class=""disabled"">« Previous</div>"
                + @"<span class=""current"">1</span>"
                + @"<span><a href=""/Posts/Page/2"">2</a></span>"
                + @"<a href=""/Posts/Page/2"" class=""next"">Next »</a>"
                + @"</div>";
            Assert.That(pager.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void CreateTwoPagePages_SecondIsCurrent_PreviousEnabled_NextDisabled()
        {
            //arrange/act
            var pager = PagerHtmlExtension.Pager(null, "/Posts/Page/{0}", 2, 2);

            //assert
            var expected = @"<div class=""pager"">"
                + @"<span><a href=""/Posts/Page/1"" class=""prev"">« Previous</a></span>"
                + @"<span><a href=""/Posts/Page/1"">1</a></span>"
                + @"<span class=""current"">2</span>"
                + @"<div class=""disabled"">Next »</div>"
                + @"</div>";

            Assert.That(pager.ToString(), Is.EqualTo(expected));
        }

        [Test]
        public void CreateTenPages_FiveIsCurrent_Range()
        {
            //arrage/act
            var pager = PagerHtmlExtension.Pager(null, "/Posts/Page/{0}", 10, 5);

            //assert
            var expected = @"<div class=""pager"">"
                + @"<span><a href=""/Posts/Page/4"" class=""prev"">« Previous</a></span>"
                + @"<span><a href=""/Posts/Page/3"">3</a></span>"
                + @"<span><a href=""/Posts/Page/4"">4</a></span>"
                + @"<span class=""current"">5</span>"
                + @"<span><a href=""/Posts/Page/6"">6</a></span>"
                + @"<span><a href=""/Posts/Page/7"">7</a></span>"
                + @"<a href=""/Posts/Page/6"" class=""next"">Next »</a></div>";

            Assert.That(pager.ToString(), Is.EqualTo(expected));

        }
    }
}
