using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Web.Areas.Admin.Controllers;
using System.Web.Mvc;
using Trackyourtasks.Core.DAL.Repositories;
using Moq;
using Trackyourtasks.Core.DAL.DataModel;
using Web.Areas.Admin.Models;

namespace Trackyourtasks.Core.Tests.Tests.Controllers.Admin
{
    [TestFixture]
    public class AdminUserManagementControllerTests
    {
        [Test]
        public void Smoke()
        {
            //arrange
            var repository = new Mock<IUsersRepository>();
            var userManagement = new AdminUserManagementController(repository.Object);

            //act/post
            Assert.That(userManagement, Is.Not.Null);
        }

        [Test]
        public void GetIndex_ReturnsView()
        {
            //arrange
            var repository = new Mock<IUsersRepository>();
            var userManagement = new AdminUserManagementController(repository.Object);

            //act
            var result = userManagement.Index() as ViewResult;

            //assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetSummary_ReturnsSummaryObject()
        {
            //arrange 
            var repository = new Mock<IUsersRepository>();
            var usersList = new List<User>
            {
                new User { Email = "1@a.com" },
                new User { Email = "2@a.com" },
                new User { Email = "3@a.com" },
                new User { Email = "4@a.com", Temp = true }
            };
            repository.Setup(r => r.Users).Returns(usersList.AsQueryable());

            var userManagement = new AdminUserManagementController(repository.Object);

            //act
            var result = userManagement.Summary() as ViewResult;

            //post
            var model = result.ViewData.Model as AdminUserSummary;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.TotalRegisteredUsers, Is.EqualTo(4));
            Assert.That(model.TempUsers, Is.EqualTo(1));
        }
    }
}
