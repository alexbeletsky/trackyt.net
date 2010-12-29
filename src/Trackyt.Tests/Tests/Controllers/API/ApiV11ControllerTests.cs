using System.Collections.Generic;
using System.Threading;
using System.Web.Mvc;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Trackyt.Core.DAL.Repositories;
using Trackyt.Core.Services;
using Web.API.v11.Controllers;
using Web.API.v11.Model;

namespace Trackyt.Core.Tests.Tests.Controllers.API
{
    // API v.1.1 is covered with integration tests (/Scripts/Tests/api/tests.api.v11.js)

    // TODO: API unit tests for all controller methods
    // TODO: API improve test by using IDateTimeProvider to be able to mock it and use mock instead of DateTime.UtcNow (test perfomance issue)
    [TestFixture]
    public class ApiV11ControllerTests
    {
        [Test]
        public void Smoke()
        {
            // assert
            var repository = new Mock<ITasksRepository>();
            var mapper = new Mock<IMappingEngine>();
            var service = new Mock<IApiService>();
            var api = new ApiV11Controller(service.Object, repository.Object, mapper.Object);

            // act / assert
            Assert.That(api, Is.Not.Null);
        }

        [Test]
        public void Start_TaskStarted_StartedTimeInitialized()
        {
            // arrange
            var userId = 100;
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var mapper = ApiTestsCommonSetup.SetupMapper();
            var service = new Mock<IApiService>();

            var api = new ApiV11Controller(service.Object, repository.Object, mapper);

            service.Setup(s => s.GetUserIdByApiToken("api_token")).Returns(userId);

            // act 
            api.Start("api_token", 1);

            // assert
            var task = ApiTestsCommonSetup.SubmittedTasks[0];
            Assert.That(task.StartedDate, Is.Not.Null, "stated date has not been set");
            Assert.That(task.StoppedDate, Is.Null, "stopped date has not been reset to null");
            Assert.That(task.Status, Is.EqualTo(1), "task status is not <started>");
        }

        [Test]
        public void Stop_StatedTaskStopped_StoppedInitialized()
        {
            // arrange
            var userId = 100;
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var mapper = ApiTestsCommonSetup.SetupMapper();
            var service = new Mock<IApiService>();

            var api = new ApiV11Controller(service.Object, repository.Object, mapper);

            service.Setup(s => s.GetUserIdByApiToken("api_token")).Returns(userId);

            // act 
            api.Start("api_token", 1);
            Thread.Sleep(1000);
            api.Stop("api_token", 1);

            // assert
            var task = ApiTestsCommonSetup.SubmittedTasks[0];
            Assert.That(task.StartedDate, Is.Not.Null, "stated date has not been set");
            Assert.That(task.StoppedDate, Is.Not.Null, "stopped date has not been set");
            Assert.That(task.Status, Is.EqualTo(2), "task status is not <stopped>");
        }

        [Test]
        public void Stop_StatedTaskStopped_ActualWorkUpdated()
        {
            // arrange
            var userId = 100;
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var mapper = ApiTestsCommonSetup.SetupMapper();
            var service = new Mock<IApiService>();

            var api = new ApiV11Controller(service.Object, repository.Object, mapper);

            service.Setup(s => s.GetUserIdByApiToken("api_token")).Returns(userId);

            // act 
            api.Start("api_token", 1);
            Thread.Sleep(1000);
            api.Stop("api_token", 1);

            // assert
            var task = ApiTestsCommonSetup.SubmittedTasks[0];
            Assert.That(task.ActualWork, Is.EqualTo(1), "one second have to be stored as actual work");
        }

        [Test]
        public void Start_IfStoppedTaskIsStartedAgain_StartedDateUpdated()
        {
            // arrange
            var userId = 100;
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var mapper = ApiTestsCommonSetup.SetupMapper();
            var service = new Mock<IApiService>();

            var api = new ApiV11Controller(service.Object, repository.Object, mapper);

            service.Setup(s => s.GetUserIdByApiToken("api_token")).Returns(userId);

            // act 
            api.Start("api_token", 1);
            Thread.Sleep(1000);
            api.Stop("api_token", 1);
            Thread.Sleep(1000);
            api.Start("api_token", 1);

            // assert
            var task = ApiTestsCommonSetup.SubmittedTasks[0];
            Assert.That(task.StartedDate, Is.Not.Null, "stated date has not been set");
            Assert.That(task.StoppedDate, Is.Null, "stopped date has not been reset to null");
            Assert.That(task.Status, Is.EqualTo(1), "task status is not <started>");
        }

        [Test]
        public void Start_IfStoppedTaskIsStartedAgain_ActualWorkStillTheSame()
        {
            // arrange
            var userId = 100;
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var mapper = ApiTestsCommonSetup.SetupMapper();
            var service = new Mock<IApiService>();

            var api = new ApiV11Controller(service.Object, repository.Object, mapper);

            service.Setup(s => s.GetUserIdByApiToken("api_token")).Returns(userId);

            // act 
            api.Start("api_token", 1);
            Thread.Sleep(1000);
            api.Stop("api_token", 1);
            Thread.Sleep(1000);
            api.Start("api_token", 1);

            // assert
            var task = ApiTestsCommonSetup.SubmittedTasks[0];
            Assert.That(task.ActualWork, Is.EqualTo(1), "one second have to be stored as actual work");
        }

        [Test]
        public void Stop_IfStoppedTaskIsStartedAgainAndStopped_StoppedUpdated()
        {
            // arrange
            var userId = 100;
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var mapper = ApiTestsCommonSetup.SetupMapper();
            var service = new Mock<IApiService>();

            var api = new ApiV11Controller(service.Object, repository.Object, mapper);

            service.Setup(s => s.GetUserIdByApiToken("api_token")).Returns(userId);

            // act 
            api.Start("api_token", 1);
            Thread.Sleep(1000);
            api.Stop("api_token", 1);
            api.Start("api_token", 1);
            Thread.Sleep(1000);
            api.Stop("api_token", 1);

            // assert
            var task = ApiTestsCommonSetup.SubmittedTasks[0];
            Assert.That(task.StoppedDate, Is.Not.Null, "stopped date initialized");
        }

        [Test]
        public void Stop_IfStoppedTaskIsStartedAgainAndStopped_ActualWorkUpdated()
        {
            // arrange
            var userId = 100;
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var mapper = ApiTestsCommonSetup.SetupMapper();
            var service = new Mock<IApiService>();

            var api = new ApiV11Controller(service.Object, repository.Object, mapper);

            service.Setup(s => s.GetUserIdByApiToken("api_token")).Returns(userId);

            // act 
            api.Start("api_token", 1);
            Thread.Sleep(1000);
            api.Stop("api_token", 1);
            api.Start("api_token", 1);
            Thread.Sleep(1000);
            api.Stop("api_token", 1);

            // assert
            var task = ApiTestsCommonSetup.SubmittedTasks[0];
            Assert.That(task.ActualWork, Is.EqualTo(2), "two seconds have to be stored as actual work");
        }

        [Test]
        public void All_IfTaskStatusIsNone_SpentEqualsToActualWork()
        {
            // arrange
            var userId = 100;
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var mapper = ApiTestsCommonSetup.SetupMapper();
            var service = new Mock<IApiService>();

            var api = new ApiV11Controller(service.Object, repository.Object, mapper);

            service.Setup(s => s.GetUserIdByApiToken("api_token")).Returns(userId);

            // act 
            var results = api.All("api_token") as JsonResult;
            dynamic data = results.Data;

            var tasksList = data.data.tasks as IList<TaskDescriptor>;
            Assert.That(tasksList, Is.Not.Null);
            Assert.That(tasksList[0].spent, Is.EqualTo(0), "spend equal to task ActualWork field");
        }

        [Test]
        public void All_IfTaskStatusIsStopped_SpentEqualsToActualWork()
        {
            // arrange
            var userId = 100;
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var mapper = ApiTestsCommonSetup.SetupMapper();
            var service = new Mock<IApiService>();

            var api = new ApiV11Controller(service.Object, repository.Object, mapper);

            service.Setup(s => s.GetUserIdByApiToken("api_token")).Returns(userId);

            // act 
            var results = api.All("api_token") as JsonResult;
            dynamic data = results.Data;

            var tasksList = data.data.tasks as IList<TaskDescriptor>;
            Assert.That(tasksList, Is.Not.Null);
            Assert.That(tasksList[2].spent, Is.EqualTo(20), "spend equal to task ActualWork field");
        }


        // TODO: API enable (correct) test then DateTimeProvider implmented
        //[Test]
        //public void All_IfTaskStatusIsStarted_SpentEqualsToActualWorkPlusTimeSpanBetweenStartedDateAndCurrentDate()
        //{
        //    // arrange
        //    var userId = 100;
        //    var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
        //    var mapper = ApiTestsCommonSetup.SetupMapper();
        //    var service = new Mock<IApiService>();

        //    var api = new ApiV11Controller(service.Object, repository.Object, mapper);

        //    service.Setup(s => s.GetUserIdByApiToken("api_token")).Returns(userId);

        //    // act 
        //    var results = api.All("api_token") as JsonResult;
        //    dynamic data = results.Data;

        //    var difference = GetTimeDifference();

        //    var tasksList = data.data.tasks as IList<TaskDescriptor>;
        //    Assert.That(tasksList, Is.Not.Null);
        //    Assert.That(tasksList[1].spent, Is.EqualTo(20 + difference), "spend equal to task ActualWork field");
        //}

        //private static int GetTimeDifference()
        //{
        //    var difference = Convert.ToInt32(Math.Floor((DateTime.Now - ApiTestsCommonSetup.StartedDate).TotalSeconds));
        //    return difference;
        //}
    }
}
