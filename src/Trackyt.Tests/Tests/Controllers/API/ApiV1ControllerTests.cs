//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Mvc;
//using AutoMapper;
//using Moq;
//using NUnit.Framework;
//using Trackyt.Core.DAL.DataModel;
//using Trackyt.Core.DAL.Repositories;
//using Trackyt.Core.Services;
//using Web.API.v1.Controllers;
//using Web.Infrastructure;
//using Web.API.v1.Model;

//namespace Trackyt.Core.Tests.Tests.Controllers.API
//{
//    [TestFixture]
//    public class ApiV1ControllerTests
//    {
//        [Test]
//        public void Smoke()
//        {
//            // arrange
//            var repository = new Mock<ITasksRepository>();
//            var mapper = new Mock<IMappingEngine>();
//            var service = new Mock<IApiService>();
//            var api = new ApiV1Controller(service.Object, repository.Object, mapper.Object);

//            // act/assert
//            Assert.That(api, Is.Not.Null);
//        }

//        [Test]
//        public void Authenticate_Success()
//        {
//            // arrange
//            var repository = new Mock<ITasksRepository>();
//            var mapper = new Mock<IMappingEngine>();
//            var service = new Mock<IApiService>();

//            var api = new ApiV1Controller(service.Object, repository.Object, mapper.Object);

//            service.Setup(s => s.GetApiToken("a@a.com", "111")).Returns("api_token");

//            // act
//            var result = api.Authenticate("a@a.com", "111") as JsonResult;
//            dynamic data = result.Data;

//            // post
//            Assert.That(data, Is.Not.Null);
//            Assert.That(data.success, Is.True);
//            Assert.That((string)data.data.apiToken, Is.EqualTo("api_token"));
//        }

//        [Test]
//        public void Authenticate_Fail()
//        {
//            // arrange
//            var repository = new Mock<ITasksRepository>();
//            var mapper = new Mock<IMappingEngine>();
//            var service = new Mock<IApiService>();

//            var api = new ApiV1Controller(service.Object, repository.Object, mapper.Object);

//            service.Setup(s => s.GetApiToken("a@a.com", "111")).Returns("api_token");

//            // act
//            var result = api.Authenticate("aaa@a.com", "111") as JsonResult;
//            dynamic data = result.Data;

//            // post
//            Assert.That(data, Is.Not.Null);
//            Assert.That(data.success, Is.False);
//            Assert.That((string)data.data.apiToken, Is.Null);
//        }

//        [Test]
//        public void GetAllTasks()
//        {
//            //arrange
//            var userId = 100;
//            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
//            var mapper = ApiTestsCommonSetup.SetupMapper();
//            var service = new Mock<IApiService>();

//            var api = new ApiV1Controller(service.Object, repository.Object, mapper);

//            service.Setup(s => s.GetUserByApiToken("api_token")).Returns(100);

//            //act
//            var result = api.GetAllTasks("api_token") as JsonResult;
//            dynamic data = result.Data;

//            //post
//            Assert.That(data, Is.Not.Null);
//            Assert.That(data.success, Is.True);

//            var tasks = data.data.tasks as IList<TaskDto>;
//            Assert.That(tasks.Count, Is.EqualTo(3));

//            Assert.That(tasks[0].Id, Is.EqualTo(1));
//            Assert.That(tasks[0].Number, Is.EqualTo(1));
//            Assert.That(tasks[0].Description, Is.EqualTo("Task1"));
//            Assert.That(tasks[0].ActualWork, Is.EqualTo(0));

//            Assert.That(tasks[1].Number, Is.EqualTo(2));
//            Assert.That(tasks[1].Number, Is.EqualTo(2));
//            Assert.That(tasks[1].Description, Is.EqualTo("Task2"));
//            Assert.That(tasks[1].ActualWork, Is.EqualTo(15));

//            Assert.That(tasks[2].Number, Is.EqualTo(3));
//            Assert.That(tasks[2].Number, Is.EqualTo(3));
//            Assert.That(tasks[2].Description, Is.EqualTo("Task3"));
//            Assert.That(tasks[2].ActualWork, Is.EqualTo(20));
//        }

//        [Test]
//        public void Submit_New_Task()
//        {
//            // arrange
//            var userId = 100;
//            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
//            var mapper = ApiTestsCommonSetup.SetupMapper();
//            var service = new Mock<IApiService>();

//            var api = new ApiV1Controller(service.Object, repository.Object, mapper);

//            service.Setup(s => s.GetUserByApiToken("api_token")).Returns(100);

//            var submit = new List<TaskDto> {
//                new TaskDto { Id = 0, ActualWork = 14, Description = "new task 1", Number = 12 },
//                new TaskDto { Id = 0, ActualWork = 177, Description = "new task 2", Number = 13 }
//            };

//            // act
//            api.Submit("api_token", submit);

//            // assert
//            Assert.That(ApiTestsCommonSetup.SubmittedTasks.Count, Is.EqualTo(2));

//            Assert.That(ApiTestsCommonSetup.SubmittedTasks[0].Id, Is.EqualTo(222));
//            Assert.That(ApiTestsCommonSetup.SubmittedTasks[0].UserId, Is.EqualTo(100));
//            Assert.That(ApiTestsCommonSetup.SubmittedTasks[0].ActualWork, Is.EqualTo(14));
//            Assert.That(ApiTestsCommonSetup.SubmittedTasks[0].Description, Is.EqualTo("new task 1"));
//            Assert.That(ApiTestsCommonSetup.SubmittedTasks[0].Number, Is.EqualTo(12));


//            Assert.That(ApiTestsCommonSetup.SubmittedTasks[1].Id, Is.EqualTo(223));
//            Assert.That(ApiTestsCommonSetup.SubmittedTasks[1].UserId, Is.EqualTo(100));
//            Assert.That(ApiTestsCommonSetup.SubmittedTasks[1].ActualWork, Is.EqualTo(177));
//            Assert.That(ApiTestsCommonSetup.SubmittedTasks[1].Description, Is.EqualTo("new task 2"));
//            Assert.That(ApiTestsCommonSetup.SubmittedTasks[1].Number, Is.EqualTo(13));
//        }

//        [Test]
//        public void Submit_Update_Result()
//        {
//            // arrange
//            var userId = 100;
//            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
//            var mapper = ApiTestsCommonSetup.SetupMapper();
//            var service = new Mock<IApiService>();

//            var api = new ApiV1Controller(service.Object, repository.Object, mapper);

//            service.Setup(s => s.GetUserByApiToken("api_token")).Returns(100);

//            var submit = new List<TaskDto> {
//                new TaskDto { Id = 0, ActualWork = 14, Description = "new task 1", Number = 12 },
//                new TaskDto { Id = 0, ActualWork = 177, Description = "new task 2", Number = 13 }
//            };

//            // act
//            var result = api.Submit("api_token", submit) as JsonResult;
//            dynamic data = result.Data;

//            // assert
//            Assert.That(data, Is.Not.Null);
//            Assert.That(data.success, Is.True);
//            Assert.That(data.data.newTasks, Is.EqualTo(2));
//            Assert.That(data.data.updatedTasks, Is.EqualTo(0));
//            Assert.That(data.data.deletedTasks, Is.EqualTo(0));
//        }

//        [Test]
//        public void Submit_Update_Task()
//        {
//            // arrange
//            var userId = 100;
//            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
//            var mapper = ApiTestsCommonSetup.SetupMapper();
//            var service = new Mock<IApiService>();

//            var api = new ApiV1Controller(service.Object, repository.Object, mapper);

//            service.Setup(s => s.GetUserByApiToken("api_token")).Returns(100);

//            var submit = new List<TaskDto> {
//                new TaskDto { Id = 1, ActualWork = 14, Description = "updated", Number = 12 },
//            };

//            // act
//            api.Submit("api_token", submit);

//            // assert
//            Assert.That(ApiTestsCommonSetup.SubmittedTasks.Count, Is.EqualTo(1));

//            Assert.That(ApiTestsCommonSetup.SubmittedTasks[0].Id, Is.EqualTo(1), "id of object could not be changed");
//            Assert.That(ApiTestsCommonSetup.SubmittedTasks[0].UserId, Is.EqualTo(100));
//            Assert.That(ApiTestsCommonSetup.SubmittedTasks[0].ActualWork, Is.EqualTo(14));
//            Assert.That(ApiTestsCommonSetup.SubmittedTasks[0].Description, Is.EqualTo("updated"));
//            Assert.That(ApiTestsCommonSetup.SubmittedTasks[0].Number, Is.EqualTo(12));
//        }

//        [Test]
//        public void Submit_Update_Task_Result()
//        {
//            // arrange
//            var userId = 100;
//            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
//            var mapper = ApiTestsCommonSetup.SetupMapper();
//            var service = new Mock<IApiService>();

//            var api = new ApiV1Controller(service.Object, repository.Object, mapper);

//            service.Setup(s => s.GetUserByApiToken("api_token")).Returns(100);

//            var submit = new List<TaskDto> {
//                new TaskDto { Id = 1, ActualWork = 14, Description = "updated", Number = 12 },
//            };

//            // act
//            var result = api.Submit("api_token", submit) as JsonResult;
//            dynamic data = result.Data;

//            // assert
//            Assert.That(data, Is.Not.Null);
//            Assert.That(data.success, Is.True);
//            Assert.That(data.data.newTasks, Is.EqualTo(0));
//            Assert.That(data.data.updatedTasks, Is.EqualTo(1));
//            Assert.That(data.data.deletedTasks, Is.EqualTo(0));
//        }

//        [Test]
//        public void Delete()
//        {
//            // arrange
//            var userId = 100;
//            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
//            var mapper = ApiTestsCommonSetup.SetupMapper();
//            var service = new Mock<IApiService>();

//            var api = new ApiV1Controller(service.Object, repository.Object, mapper);

//            service.Setup(s => s.GetUserByApiToken("api_token")).Returns(100);

//            var delete = new List<TaskDto> {
//                new TaskDto { Id = 1, ActualWork = 14, Description = "updated", Number = 12 },
//            };

//            // act
//            api.Delete("api_token", delete);

//            // post
//            Assert.That(ApiTestsCommonSetup.DeletedTasks.Count, Is.EqualTo(1));
//            Assert.That(ApiTestsCommonSetup.DeletedTasks[0].Id, Is.EqualTo(1));
//        }

//        [Test]
//        public void Delete_Result()
//        {
//            // arrange
//            var userId = 100;
//            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
//            var mapper = ApiTestsCommonSetup.SetupMapper();
//            var service = new Mock<IApiService>();

//            var api = new ApiV1Controller(service.Object, repository.Object, mapper);

//            service.Setup(s => s.GetUserByApiToken("api_token")).Returns(100);

//            var delete = new List<TaskDto> {
//                new TaskDto { Id = 1, ActualWork = 14, Description = "updated", Number = 12 },
//            };

//            // act
//            var result = api.Delete("api_token", delete) as JsonResult;
//            dynamic data = result.Data;

//            // assert
//            Assert.That(data, Is.Not.Null);
//            Assert.That(data.success, Is.True);
//            Assert.That(data.data.newTasks, Is.EqualTo(0));
//            Assert.That(data.data.updatedTasks, Is.EqualTo(0));
//            Assert.That(data.data.deletedTasks, Is.EqualTo(1));
//        }
//    }
//}
