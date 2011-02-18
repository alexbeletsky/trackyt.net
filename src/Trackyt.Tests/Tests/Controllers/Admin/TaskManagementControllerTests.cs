using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using Trackyt.Core.DAL.Repositories;
using Web.Areas.Admin.Controllers;
using System.Web.Mvc;
using Web.Areas.Admin.Models;
using Trackyt.Core.DAL.DataModel;

namespace Trackyt.Core.Tests.Tests.Controllers.Admin
{
    [TestFixture]
    public class TaskManagementControllerTests
    {
        [Test]
        public void Summary_Get_ReturnsTotalTasks()
        {
            // arrange
            var tasksRepository = new Mock<ITasksRepository>();
            var controller = new TaskManagementController(tasksRepository.Object);

            var tasks = new List<Task>
            {
                new Task { Id = 1, ActualWork = 0 },
                new Task { Id = 2, ActualWork = 0 },
                new Task { Id = 3, ActualWork = 0 }
            };

            tasksRepository.Setup(t => t.Tasks).Returns(tasks.AsQueryable());

            // act
            var result = controller.Summary() as ViewResult;

            // assert
            var model = result.ViewData.Model as TaskSummaryModel;
            Assert.That(model.TotalTasks, Is.EqualTo(3));
        }

        [Test]
        public void Summary_Get_ReturnsTotalLoggedTime()
        {
            // arrange
            var tasksRepository = new Mock<ITasksRepository>();
            var controller = new TaskManagementController(tasksRepository.Object);

            var tasks = new List<Task>
            {
                new Task { Id = 1, ActualWork = 10 },
                new Task { Id = 2, ActualWork = 15 },
                new Task { Id = 3, ActualWork = 20 }
            };

            tasksRepository.Setup(t => t.Tasks).Returns(tasks.AsQueryable());

            // act
            var result = controller.Summary() as ViewResult;

            // assert
            var model = result.ViewData.Model as TaskSummaryModel;
            Assert.That(model.TotalLoggedTime, Is.EqualTo(45));
        }
    }
}
