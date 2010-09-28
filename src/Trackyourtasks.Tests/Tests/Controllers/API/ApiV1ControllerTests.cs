using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Web.Controllers;
using Moq;
using Trackyourtasks.Core.DAL.Repositories;
using Trackyourtasks.Core.DAL.DataModel;
using System.Web.Mvc;
using AutoMapper;
using Web.API.v1.Model;
using Web.Infrastructure;

namespace Trackyourtasks.Core.Tests.Tests.Controllers.API
{
    [TestFixture]
    public class ApiV1ControllerTests
    {
        #region setup code

        private static IList<Task> _submittedTasks = new List<Task>();
        private static IList<Task> _deletedTasks = new List<Task>();

        private static IMappingEngine SetupMapper()
        {
            TrackyMapping.SetupMapping();

            return Mapper.Engine;
        }

        private static Mock<ITasksRepository> SetupMockRepository(int userId)
        {
            var repository = new Mock<ITasksRepository>();
            repository.Setup(f => f.GetTasks()).Returns(new List<Task>()
                {
                    new Task { Id = 1, Description = "Task1", ActualWork = 0, Number = 1, UserId = userId },
                    new Task { Id = 2, Description = "Task2", ActualWork = 15, Number = 2, UserId = userId },
                    new Task { Id = 3, Description = "Task3", ActualWork = 20, Number = 3, UserId = userId },
                    new Task { Id = 4, Description = "Task3", ActualWork = 0, Number = 4, UserId = userId + 1 },
                    new Task { Id = 5, Description = "Task3", ActualWork = 7, Number = 1, UserId = userId + 2 },
                    new Task { Id = 6, Description = "Task3", ActualWork = 4, Number = 1, UserId = userId + 3 },
                }.AsQueryable()
                );

            var index = 222;
            _submittedTasks.Clear();
            _deletedTasks.Clear();
            repository.Setup(f => f.SaveTask(It.IsAny<Task>())).Callback((Task t) => 
                {
                    //assign id for new tasks
                    if (t.Id == 0)
                    {
                        t.Id = index++;
                    }
                    _submittedTasks.Add(t);
                }
            );
            repository.Setup(f => f.DeleteTask(It.IsAny<Task>())).Callback((Task t) =>
                {
                    _deletedTasks.Add(t);
                }
            );

            return repository;
        }

        #endregion

        [Test]
        public void Smoke()
        {
            //arrange
            var repository = new Mock<ITasksRepository>();
            var mapper = new Mock<IMappingEngine>();
            var api = new ApiV1Controller(repository.Object, mapper.Object);
            //act/assert
            Assert.That(api, Is.Not.Null);
        }

        [Test]
        public void GetAllTasks()
        {
            //arrange
            var userId = 100;
            var repository = SetupMockRepository(userId);
            var mapper = SetupMapper();
            var api = new ApiV1Controller(repository.Object, mapper);

            //act
            var result = api.GetAllTasks(userId) as JsonResult;

            //post
            IList<TaskDto> data = result.Data as IList<TaskDto>;
            Assert.That(data.Count, Is.EqualTo(3));

            Assert.That(data[0].Id, Is.EqualTo(1));
            Assert.That(data[0].Number, Is.EqualTo(1));
            Assert.That(data[0].Description, Is.EqualTo("Task1"));
            Assert.That(data[0].ActualWork, Is.EqualTo(0));

            Assert.That(data[1].Number, Is.EqualTo(2));
            Assert.That(data[1].Number, Is.EqualTo(2));
            Assert.That(data[1].Description, Is.EqualTo("Task2"));
            Assert.That(data[1].ActualWork, Is.EqualTo(15));

            Assert.That(data[2].Number, Is.EqualTo(3));
            Assert.That(data[2].Number, Is.EqualTo(3));
            Assert.That(data[2].Description, Is.EqualTo("Task3"));
            Assert.That(data[2].ActualWork, Is.EqualTo(20));
        }

        [Test]
        public void Submit_New_Task()
        {
            //arrange
            var userId = 100;
            var repository = SetupMockRepository(userId);
            var mapper = SetupMapper();
            var api = new ApiV1Controller(repository.Object, mapper);
            var submit = new List<TaskDto> {
                new TaskDto { Id = 0, ActualWork = 14, Description = "new task 1", Number = 12 },
                new TaskDto { Id = 0, ActualWork = 177, Description = "new task 2", Number = 13 }
            };
         
            //act
            api.Submit(userId, submit);

            //assert
            Assert.That(_submittedTasks.Count, Is.EqualTo(2));

            Assert.That(_submittedTasks[0].Id, Is.EqualTo(222));
            Assert.That(_submittedTasks[0].UserId, Is.EqualTo(100));
            Assert.That(_submittedTasks[0].ActualWork, Is.EqualTo(14));
            Assert.That(_submittedTasks[0].Description, Is.EqualTo("new task 1"));
            Assert.That(_submittedTasks[0].Number, Is.EqualTo(12));


            Assert.That(_submittedTasks[1].Id, Is.EqualTo(223));
            Assert.That(_submittedTasks[1].UserId, Is.EqualTo(100));
            Assert.That(_submittedTasks[1].ActualWork, Is.EqualTo(177));
            Assert.That(_submittedTasks[1].Description, Is.EqualTo("new task 2"));
            Assert.That(_submittedTasks[1].Number, Is.EqualTo(13));
        }

        [Test]
        public void Submit_Update_Task()
        {
            //arrange
            var userId = 100;
            var repository = SetupMockRepository(userId);
            var mapper = SetupMapper();
            var api = new ApiV1Controller(repository.Object, mapper);
            var submit = new List<TaskDto> {
                new TaskDto { Id = 1, ActualWork = 14, Description = "updated", Number = 12 },
            };

            //act
            api.Submit(userId, submit);

            //assert
            Assert.That(_submittedTasks.Count, Is.EqualTo(1));

            Assert.That(_submittedTasks[0].Id, Is.EqualTo(1), "id of object could not be changed");
            Assert.That(_submittedTasks[0].UserId, Is.EqualTo(100));
            Assert.That(_submittedTasks[0].ActualWork, Is.EqualTo(14));
            Assert.That(_submittedTasks[0].Description, Is.EqualTo("updated"));
            Assert.That(_submittedTasks[0].Number, Is.EqualTo(12));
        }

        [Test]
        public void Delete()
        {
            //arrange
            var userId = 100;
            var repository = SetupMockRepository(userId);
            var mapper = SetupMapper();
            var api = new ApiV1Controller(repository.Object, mapper);
            var delete = new List<TaskDto> {
                new TaskDto { Id = 1, ActualWork = 14, Description = "updated", Number = 12 },
            };

            //act
            api.Delete(userId, delete);

            //post
            Assert.That(_deletedTasks.Count, Is.EqualTo(1));
            Assert.That(_deletedTasks[0].Id, Is.EqualTo(1));
        }
    }
}
