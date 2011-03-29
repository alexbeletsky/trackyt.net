using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Trackyt.Core.DAL.Repositories.Impl;
using Trackyt.Core.Tests.Framework;
using Trackyt.Core.DAL.DataModel;
using Trackyt.Core.DAL.Extensions;

namespace Trackyt.Core.DAL.Tests
{
    [TestFixture]
    public class TasksRepositoryTests
    {
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
                    UserId = fixture.Setup.User.Id,
                    Description = "My new task",
                    Status = 0,
                    ActualWork = 0
                };

                //ACT
                repository.Save(task);

                //POST
                var foundTask = repository.Tasks.WithId(task.Id);
                Assert.That(foundTask, Is.Not.Null);
                Assert.That(foundTask.UserId, Is.EqualTo(fixture.Setup.User.Id));
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
                    UserId = fixture.Setup.User.Id,
                    Description = "My new task",
                    Status = 0,
                    ActualWork = 0
                };

                repository.Save(task);

                //ACT
                repository.Delete(task);

                //POST
                var foundTask = repository.Tasks.WithId(task.Id);
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
                    UserId = fixture.Setup.User.Id,
                    Description = "My new task",
                    Status = 0,
                    ActualWork = 0
                };

                repository.Save(task);

                //ACT
                task.Description = "My new task (update)";
                repository.Save(task);

                //POST
                var foundTask = repository.Tasks.WithId(task.Id);
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
                    UserId = fixture.Setup.User.Id,
                    Description = "My new task",
                    Status = 0,
                    ActualWork = 0
                };

                repository.Save(task);

                //ACT
                var foundTask = repository.Tasks.WithId(task.Id);

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
                    UserId = fixture.Setup.User.Id,
                    Description = "My new task",
                    Status = 0,
                    ActualWork = 0
                };

                repository.Save(task);

                //ACT
                var foundTask = repository.Tasks.WithUserId(fixture.Setup.User.Id);

                //POST
                Assert.That(foundTask, Is.Not.Null);
                Assert.That(foundTask.Count(), Is.EqualTo(1));
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
                    new Task() { UserId = fixture.Setup.User.Id, Description="test1" } , 
                    new Task() { UserId = fixture.Setup.User.Id, Description="test2" }
                };

                foreach (var task in tasks)
                {
                    repository.Save(task);
                }

                //ACT
                var foundTasks = repository.Tasks;

                //POST
                Assert.That(foundTasks, Is.Not.Null);
                Assert.That(foundTasks.Count(), Is.EqualTo(2));
            }
        }

        [Test]
        public void Save_CreatedDateInitialized_TaskSavedWithCreatedDate()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                // arrange
                var repository = new TasksRepository(fixture.Setup.Context);

                // act
                var task = new Task { UserId = fixture.Setup.User.Id, Description = "created date test" };
                repository.Save(task);

                // assert
                var found = repository.Tasks.Where(t => t.Description == "created date test").SingleOrDefault();
                Assert.That(found.CreatedDate, Is.Not.Null);
            }
        }

        [Test]
        public void Tasks_SortedByPosition()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                // arrange
                var repository = new TasksRepository(fixture.Setup.Context);

                repository.Save(new Task { UserId = fixture.Setup.User.Id, Position = 3 });
                repository.Save(new Task { UserId = fixture.Setup.User.Id, Position = 2 });
                repository.Save(new Task { UserId = fixture.Setup.User.Id, Position = 1 });

                // act
                var tasks = repository.Tasks.ToArray();

                // assert
                Assert.That(tasks[0].Position, Is.EqualTo(1));
                Assert.That(tasks[1].Position, Is.EqualTo(2));
                Assert.That(tasks[2].Position, Is.EqualTo(3));
            }
        }

        [Test]
        public void Tasks_DoneFlag_DefaultIsFalse()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                // arrange
                var repository = new TasksRepository(fixture.Setup.Context);

                var task = new Task
                {
                    Description = "Test task",
                    UserId = fixture.Setup.User.Id
                };

                // act
                repository.Save(task);

                // assert
                var savedTask = repository.Tasks.Where(t => t.Id == task.Id).SingleOrDefault();
                Assert.That(savedTask.Done, Is.False);
            }
        }

        [Test]
        public void Tasks_DoneFlag_Set()
        {
            using (var fixture = new FixtureInit("http://localhost"))
            {
                // arrange
                var repository = new TasksRepository(fixture.Setup.Context);

                var task = new Task
                {
                    Description = "Test task",
                    UserId = fixture.Setup.User.Id,
                    Done = true
                };

                // act
                repository.Save(task);

                // assert
                var savedTask = repository.Tasks.Where(t => t.Id == task.Id).SingleOrDefault();
                Assert.That(savedTask.Done, Is.True);
            }

        }
    }
}
