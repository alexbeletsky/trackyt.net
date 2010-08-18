using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Trackyourtasks.Core.DAL.Repositories.Impl;
using Trackyourtasks.Core.Tests.Framework;
using Trackyourtasks.Core.DAL.DataModel;

namespace Trackyourtasks.Core.DAL.Tests
{
    [TestFixture]
    public class TasksRepositoryTests
    {
        [Test]
        public void Smoke()
        {
            var r = new TasksRepository();
            Assert.That(r, Is.Not.Null);
        }

        [Test]
        public void SaveTask()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //INIT
                var repository = new TasksRepository(fixture.Setup.Context);

                var task = new Task()
                {
                    Number = 0,
                    UserId = 10,
                    Description = "My new task",
                    Status = 0,
                    ActualWork = 0
                };

                //ACT
                repository.SaveTask(task);

                //POST
                var foundTask = repository.FindTaskById(task.Id);
                Assert.That(foundTask, Is.Not.Null);
                Assert.That(foundTask.UserId, Is.EqualTo(10));
                Assert.That(foundTask.Description, Is.EqualTo("My new task"));
            }
        }

        [Test]
        public void DeleteTasks()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //INIT
                var repository = new TasksRepository(fixture.Setup.Context);

                var task = new Task()
                {
                    Number = 0,
                    UserId = 10,
                    Description = "My new task",
                    Status = 0,
                    ActualWork = 0
                };

                repository.SaveTask(task);

                //ACT
                repository.DeleteTask(task);

                //POST
                var foundTask = repository.FindTaskById(task.Id);
                Assert.That(foundTask, Is.Null);
            }
        }

        [Test]
        public void UpdateTask()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //INIT
                var repository = new TasksRepository(fixture.Setup.Context);

                var task = new Task()
                {
                    Number = 0,
                    UserId = 10,
                    Description = "My new task",
                    Status = 0,
                    ActualWork = 0
                };

                repository.SaveTask(task);

                //ACT
                task.Description = "My new task (update)";
                repository.SaveTask(task);

                //POST
                var foundTask = repository.FindTaskById(task.Id);
                Assert.That(foundTask, Is.Not.Null);
                Assert.That(foundTask.Description, Is.EqualTo("My new task (update)"));
            }

        }


        [Test]
        public void FindTaskById()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //INIT
                var repository = new TasksRepository(fixture.Setup.Context);

                var task = new Task()
                {
                    Number = 0,
                    UserId = 10,
                    Description = "My new task",
                    Status = 0,
                    ActualWork = 0
                };

                repository.SaveTask(task);

                //ACT
                var foundTask = repository.FindTaskById(task.Id);

                //POST
                Assert.That(foundTask, Is.Not.Null);
            }
        }

        [Test]
        public void FindTaskByUserId()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //INIT
                var repository = new TasksRepository(fixture.Setup.Context);

                var task = new Task()
                {
                    Number = 0,
                    UserId = 10,
                    Description = "My new task",
                    Status = 0,
                    ActualWork = 0
                };

                repository.SaveTask(task);

                //ACT
                var foundTask = repository.FindTaskByUserId(10);

                //POST
                Assert.That(foundTask, Is.Not.Null);
            }
        }

        [Test]
        public void GetAllTasks()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                //INIT
                var repository = new TasksRepository(fixture.Setup.Context);
                var tasks = new[] { 
                    new Task() { Description="test1" } , 
                    new Task() { Description="test2" }
                };

                foreach (var task in tasks)
                {
                    repository.SaveTask(task);
                }

                //ACT
                var foundTasks = repository.GetAllTasks();

                //POST
                Assert.That(foundTasks, Is.Not.Null);
                Assert.That(foundTasks.Count(), Is.EqualTo(2));
            }
        }
    }
}
