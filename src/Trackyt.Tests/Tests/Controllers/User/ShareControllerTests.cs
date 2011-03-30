using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using SharpTestsEx;
using Trackyt.Core.DAL.DataModel;
using Trackyt.Core.DAL.Repositories;
using Trackyt.Core.Services;
using Web.Areas.User.Controllers;
using Trackyt.Core.Services.Impl;

namespace Trackyt.Core.Tests.Tests.Controllers.Tracky
{
    [TestFixture]
    public class ShareControllerTests
    {
        [Test]
        public void Index_ViewBagContainsUserEmail()
        {
            // arrange
            var tasksRepository = new Mock<ITasksRepository>();
            var usersRepository = new Mock<IUsersRepository>();
            var pathHelper = new Mock<IPathHelper>();
            var hashService = new HashService();
            var shareService = new ShareService(pathHelper.Object, hashService);
            var controller = new ShareController(shareService, tasksRepository.Object, usersRepository.Object);

            var tasks = new List<Task> 
            {
                new Task { Id = 0, UserId = 11,  Description = "Task 0" },
                new Task { Id = 1, UserId = 11, Description = "Task 1" },
                new Task { Id = 2, UserId = 11, Description = "Task 2" }
            };
            tasksRepository.Setup(r => r.Tasks).Returns(tasks.AsQueryable());

            var users = new List<User> 
            {
                new User { Email = "email@com.com", Id = 11 }
            };
            usersRepository.Setup(u => u.Users).Returns(users.AsQueryable());

            // act
            var email = "email@com.com";
            var correctHash = hashService.CreateMD5Hash(email + "shared_tasks");
            var result = controller.Index(email, correctHash) as ViewResult;

            // assert
            var emailResult = result.ViewBag.Email as string;
            emailResult.Should().Be(email);

        }

        [Test]
        public void Index_KeyValueIsNull_RedirectedToError()
        {
            // arrange
            var tasksRepository = new Mock<ITasksRepository>();
            var usersRepository = new Mock<IUsersRepository>();
            var pathHelper = new Mock<IPathHelper>();
            var hashService = new HashService();
            var shareService = new ShareService(pathHelper.Object, hashService);
            var controller = new ShareController(shareService, tasksRepository.Object, usersRepository.Object);

            // act
            var email = "email@com.com";
            var result = controller.Index(email, null) as RedirectToRouteResult;

            // assert
            var action = result.RouteValues["action"] as string;
            action.Should().Be("error");
        }

        [Test]
        public void Index_KeyHashIsWrong_RedirectedToError()
        {
            // arrange
            var tasksRepository = new Mock<ITasksRepository>();
            var usersRepository = new Mock<IUsersRepository>();
            var pathHelper = new Mock<IPathHelper>();
            var hashService = new HashService();
            var shareService = new ShareService(pathHelper.Object, hashService);
            var controller = new ShareController(shareService, tasksRepository.Object, usersRepository.Object);

            // act
            var result = controller.Index("email", "this_is_wrong_hash") as RedirectToRouteResult;

            // assert
            var action = result.RouteValues["action"] as string;
            action.Should().Be("error");
        }

        [Test]
        public void Index_KeyIsOk_ShowTasks()
        {
            // arrange
            var tasksRepository = new Mock<ITasksRepository>();
            var usersRepository = new Mock<IUsersRepository>();
            var pathHelper = new Mock<IPathHelper>();
            var hashService = new HashService();
            var shareService = new ShareService(pathHelper.Object, hashService);
            var controller = new ShareController(shareService, tasksRepository.Object, usersRepository.Object);

            var tasks = new List<Task> 
            {
                new Task { Id = 0, UserId = 11,  Description = "Task 0" },
                new Task { Id = 1, UserId = 11, Description = "Task 1" },
                new Task { Id = 2, UserId = 11, Description = "Task 2" }
            };
            tasksRepository.Setup(r => r.Tasks).Returns(tasks.AsQueryable());

            var email = "email@com.com";
            var users = new List<User> 
            {
                new User { Email = email, Id = 11 }
            };
            usersRepository.Setup(u => u.Users).Returns(users.AsQueryable());

            // act
            var correctHash = hashService.CreateMD5Hash(email + "shared_tasks");
            var result = controller.Index(email, correctHash) as ViewResult;

            // assert
            var data = result.Model as IList<Task>;
            data.Count.Should().Be(3);
        }
    }
}
