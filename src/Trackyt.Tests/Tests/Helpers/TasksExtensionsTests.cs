using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpTestsEx;
using Trackyt.Core.DAL.DataModel;
using Trackyt.Core.DAL.Extensions;

namespace Trackyt.Core.Tests.Tests.Helpers
{
    [TestFixture]
    public class TasksExtensionsTests
    {
        [Test]
        public void TasksExtensions_WithUserId_ReturnsUndoneTasks()
        {
            // arrange
            var tasks = new List<Task> 
            {
                new Task { Id = 0, UserId = 3, Description = "Task 0", Done = false },
                new Task { Id = 1, UserId = 3, Description = "Task 1", Done = false },
                new Task { Id = 2, UserId = 3, Description = "Task 2", Done = false },
                new Task { Id = 3, UserId = 3, Description = "Task 3", Done = true }
            };

            // act
            var returnedTasks = tasks.AsQueryable().WithUserId(3).ToList(); 
  
            // assert
            returnedTasks.Count.Should().Be(3);
        }

        [Test]
        public void TasksExtensions_WithUserIdDone_ReturnsAllDoneTasks()
        {
            // arrange
            var tasks = new List<Task> 
            {
                new Task { Id = 0, UserId = 3, Description = "Task 0", Done = false },
                new Task { Id = 1, UserId = 3, Description = "Task 1", Done = false },
                new Task { Id = 2, UserId = 3, Description = "Task 2", Done = false },
                new Task { Id = 3, UserId = 3, Description = "Task 3", Done = true }
            };

            // act
            var returnedTasks = tasks.AsQueryable().WithUserIdAndDone(3).ToList();

            // assert
            returnedTasks.Count.Should().Be(1);
        }
    }
}
