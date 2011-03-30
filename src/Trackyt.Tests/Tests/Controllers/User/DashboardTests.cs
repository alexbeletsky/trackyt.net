using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Web.Areas.User.Controllers;
using Moq;
using Trackyt.Core.DAL.Repositories;
using Web.Infrastructure.Security;
using Trackyt.Core.DAL.DataModel;
using System.Web.Mvc;
using Trackyt.Core.Security;
using Trackyt.Core.Services;

namespace Trackyt.Core.Tests.Tests.Controllers.Tracky
{
    [TestFixture]
    public class DashboardTests
    {
        [Test]
        public void Smoke()
        {
            //arrange
            var users = new Mock<IUsersRepository>();
            var path = new Mock<IPathHelper>();
            var dashboard = new DashboardController(users.Object, path.Object);

            //act/post
            Assert.That(dashboard, Is.Not.Null);
        }

        // UserId is no longer API parameter
        //[Test]
        //public void Index_Get_Initialize_UserId()
        //{
        //    //arrange
        //    var users = new Mock<IUsersRepository>();
        //    var forms = new Mock<IFormsAuthentication>();
        //    var path = new Mock<IPathHelper>();

        //    forms.Setup(f => f.GetLoggedUser()).Returns("logged@tracky.net");
        //    users.Setup(u => u.Users).Returns((new List<User> { new User { Id = 100, Email = "logged@tracky.net" } }).AsQueryable());

        //    var dashboard = new DashboardController(users.Object, forms.Object, path.Object);

        //    //act
        //    var result = dashboard.Index() as ViewResult;

        //    //post
        //    Assert.That(result.ViewData["UserId"], Is.EqualTo(100));
        //}

        [Test]
        public void Index_Get_Initialize_Api_Token()
        {
            // arrange
            var users = new Mock<IUsersRepository>();
            var path = new Mock<IPathHelper>();
            var dashboard = new DashboardController(users.Object, path.Object);

            users.Setup(u => u.Users).Returns((new List<User> { new User { Id = 100, Email = "logged@tracky.net", ApiToken = "111222" } }).AsQueryable());

            // act
            var result = dashboard.Index("logged@tracky.net") as ViewResult;

            // post
            Assert.That(result.ViewData["ApiToken"], Is.EqualTo("111222"));
        }

        [Test]
        public void Index_Get_Initialize_Api_Path()
        {
            // arrange
            var users = new Mock<IUsersRepository>();
            var path = new Mock<IPathHelper>();
            var dashboard = new DashboardController(users.Object, path.Object);

            users.Setup(u => u.Users).Returns((new List<User> { new User { Id = 100, Email = "logged@tracky.net" } }).AsQueryable());
            path.Setup(p => p.VirtualToAbsolute(It.IsAny<string>())).Returns((string v) => v);

            //act
            var result = dashboard.Index("logged@tracky.net") as ViewResult;

            //post
            Assert.That(result.ViewData["Api"], Is.EqualTo("~/API/v1.1/"));
        }

        [Test]
        public void Index_Get_Initialize_Users_Email()
        {
            // arrange
            var users = new Mock<IUsersRepository>();
            var path = new Mock<IPathHelper>();
            var dashboard = new DashboardController(users.Object, path.Object);

            users.Setup(u => u.Users).Returns((new List<User> { new User { Id = 100, Email = "logged@tracky.net" } }).AsQueryable());
            path.Setup(p => p.VirtualToAbsolute(It.IsAny<string>())).Returns((string v) => v);

            //act
            var result = dashboard.Index("logged@tracky.net") as ViewResult;

            //post
            Assert.That(result.ViewData["Email"], Is.EqualTo("logged@tracky.net"));
        }

    }
}
