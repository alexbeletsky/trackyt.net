using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Trackyt.Core.Services;
using Web.API.v11.Controllers;
using Web.API.v11.Model;
using Web.Infrastructure.Exceptions;
using SharpTestsEx;
using Trackyt.Core.DAL.DataModel;
using System.Linq;

namespace Trackyt.Core.Tests.Tests.Controllers.API
{
    // API v.1.1 is covered with integration tests (/Scripts/Tests/api/tests.api.v11.js)

    [TestFixture]
    public class ApiV11ControllerTests
    {
        [Test]
        public void Smoke()
        {
            // assert
            var repository = ApiTestsCommonSetup.SetupMockRepository(0);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            // act / assert
            Assert.That(api, Is.Not.Null);
        }

        [Test]
        public void Start_TaskStarted_StartedTimeInitialized()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = userId });

            // act 
            api.Start(token, 1);

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
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = userId});

            // act 
            api.Start(token, 1);
            api.Stop(token, 1);

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
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            var currentDate = DateTime.UtcNow;
            date.Setup(d => d.UtcNow).Returns(currentDate);
            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = userId});

            // act 
            api.Start(token, 1);
            date.Setup(d => d.UtcNow).Returns(currentDate.AddSeconds(1));            
            api.Stop(token, 1);

            // assert
            var task = ApiTestsCommonSetup.SubmittedTasks[0];
            Assert.That(task.ActualWork, Is.EqualTo(1), "one second have to be stored as actual work");
        }

        [Test]
        public void Start_IfStoppedTaskIsStartedAgain_StartedDateUpdated()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = userId });

            // act 
            api.Start(token, 1);
            api.Stop(token, 1);
            api.Start(token, 1);

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
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            var currentDate = DateTime.UtcNow;
            date.Setup(d => d.UtcNow).Returns(currentDate);
            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = userId });


            // act 
            api.Start(token, 1);
            date.Setup(d => d.UtcNow).Returns(currentDate.AddSeconds(1));
            api.Stop(token, 1);
            date.Setup(d => d.UtcNow).Returns(currentDate.AddSeconds(2));
            api.Start(token, 1);

            // assert
            var task = ApiTestsCommonSetup.SubmittedTasks[0];
            Assert.That(task.ActualWork, Is.EqualTo(1), "one second have to be stored as actual work");
        }

        [Test]
        public void Stop_IfStoppedTaskIsStartedAgainAndStopped_StoppedUpdated()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            var currentDate = DateTime.UtcNow;
            date.Setup(d => d.UtcNow).Returns(currentDate);
            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = userId });

            // act 
            api.Start(token, 1);
            date.Setup(d => d.UtcNow).Returns(currentDate.AddSeconds(1));
            api.Stop(token, 1);
            api.Start(token, 1);
            date.Setup(d => d.UtcNow).Returns(currentDate.AddSeconds(2));
            api.Stop(token, 1);

            // assert
            var task = ApiTestsCommonSetup.SubmittedTasks[0];
            Assert.That(task.StoppedDate, Is.Not.Null, "stopped date initialized");
        }

        [Test]
        public void Stop_IfStoppedTaskIsStartedAgainAndStopped_ActualWorkUpdated()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            var currentDate = DateTime.UtcNow;
            date.Setup(d => d.UtcNow).Returns(currentDate);
            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = userId });

            // act 
            api.Start(token, 1);
            date.Setup(d => d.UtcNow).Returns(currentDate.AddSeconds(1));

            api.Stop(token, 1);
            api.Start(token, 1);
            date.Setup(d => d.UtcNow).Returns(currentDate.AddSeconds(2));

            api.Stop(token, 1);

            // assert
            var task = ApiTestsCommonSetup.SubmittedTasks[0];
            Assert.That(task.ActualWork, Is.EqualTo(2), "two seconds have to be stored as actual work");
        }

        [Test]
        public void All_IfTaskStatusIsNone_SpentEqualsToActualWork()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            date.Setup(d => d.UtcNow).Returns(DateTime.UtcNow);
            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = userId });

            // act 
            var results = api.All(token) as JsonResult;
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
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            date.Setup(d => d.UtcNow).Returns(DateTime.UtcNow);
            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = userId });

            // act 
            var results = api.All(token) as JsonResult;
            dynamic data = results.Data;

            var tasksList = data.data.tasks as IList<TaskDescriptor>;
            Assert.That(tasksList, Is.Not.Null);
            Assert.That(tasksList[2].spent, Is.EqualTo(20), "spend equal to task ActualWork field");
        }


        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CheckArguments_ApiTokenWrong()
        {
            // arrange
            var userId = 100;
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken("bad_token")).Returns(new User { Id = userId });


            // act 
            var results = api.All("bad_token") as JsonResult;
            dynamic data = results.Data;

            // assert
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Autenticate_EmailIsNull_ExceptionThrown()
        {
            // arrange
            var userId = 100;
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            // act
            api.Authenticate(null, "");
        }


        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Autenticate_PasswordIsNull_ExceptionThrown()
        {
            // arrange
            var userId = 100;
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            // act
            api.Authenticate("aa", null);
        }

        [Test]
        [ExpectedException(typeof(UserNotAuthorizedException))]
        public void Authenticate_ApiTokenNotLinkedToUser_ExceptionThrow()
        {
            // arrange
            var userId = 100;
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetApiToken("email", "password")).Returns((string)null);

            // act
            api.Authenticate("email", "token");
        }

        [Test]
        [ExpectedException(typeof(Exception))]        
        public void All_CheckAuthentication_ExceptionThrown()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Throws(new Exception());

            // act
            api.All(token);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_CheckArgumentsBadToken_ExceptionThrown()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = 0 });

            // act
            api.Add("bad_token", "");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Add_CheckArgumentsBadDescription_ExceptionThrown()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = 0 });

            // act
            api.Add(token, "");
        }


        [Test]
        [ExpectedException(typeof(Exception))]
        public void Add_CheckAuthentication_ExceptionThrown()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Throws(new Exception());

            // act
            api.Add(token, "desc");
        }


        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_CheckArgumentsBadToken_ExceptionThrown()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = 0 });

            // act
            api.Delete("bad_token", 0);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Delete_CheckArgumentsBadTaskId_ExceptionThrown()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = 0 });

            // act
            api.Delete(token, -1);
        }


        [Test]
        [ExpectedException(typeof(Exception))]
        public void Delete_CheckAuthentication_ExceptionThrown()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = 0 });

            // act
            api.Delete(token, 0);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void Delete_TaskWithSuchIdDoesNotExist_ExceptionThrown()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = userId });

            // act
            api.Delete(token, 333);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Start_CheckArgumentsBadToken_ExceptionThrown()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = 0 });

            // act
            api.Start("bad_token", 0);
        }


        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Start_CheckArgumentsBadTaskId_ExceptionThrown()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = 0 });

            // act
            api.Start(token, -1);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void Start_CheckAuthentication_ExceptionThrown()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = 0 });

            // act
            api.Start(token, 0);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void Start_TaskWithSuchIdDoesNotExist_ExceptionThrown()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = userId });

            // act
            api.Start(token, 333);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Stop_CheckArgumentsBadToken_ExceptionThrown()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = 0 });

            // act
            api.Stop("bad_token", 0);
        }


        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Stop_CheckArgumentsBadTaskId_ExceptionThrown()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = 0 });

            // act
            api.Stop(token, -1);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void Stop_CheckAuthentication_ExceptionThrown()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = 0});

            // act
            api.Stop(token, 0);
        }

        [Test]
        [ExpectedException(typeof(Exception))]
        public void Stop_TaskWithSuchIdDoesNotExist_ExceptionThrown()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = userId});

            // act
            api.Stop(token, 333);
        }

        [Test]
        public void UpdatePosition_UpdateTaskPosition()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = userId});

            // act
            api.UpdatePosition(token, 1, 100);

            // assert
            var task = ApiTestsCommonSetup.SubmittedTasks[0];
            Assert.That(task.Position, Is.EqualTo(100));
        }

        [Test]
        public void UpdateDescription_UpdateTaskDescription()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = userId});

            // act
            api.UpdateDescription(token, 1, "new description");

            // assert
            var task = ApiTestsCommonSetup.SubmittedTasks[0];
            Assert.That(task.Description, Is.EqualTo("new description"));
        }

        [Test]
        public void UpdatePlannedDate_DateProvided_Planned()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = userId});

            // act
            api.UpdatePlannedDate(token, 1, "01-01-2011");

            // assert
            var task = ApiTestsCommonSetup.SubmittedTasks[0];
            task.PlannedDate.Should().Be(new DateTime(2011, 1, 1));       
        }

        [Test]
        public void UpdatePlannedDate_CheckFormat_Planned()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = userId});

            // act
            api.UpdatePlannedDate(token, 1, "12-12-2011");

            // assert
            var task = ApiTestsCommonSetup.SubmittedTasks[0];
            task.PlannedDate.Should().Be(new DateTime(2011, 12, 12));
        }

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void UpdatePlannedDate_WrongFormat_ExceptionThrown()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = userId});

            // act
            api.UpdatePlannedDate(token, 1, "12-17-2011");
        }

        [Test]
        public void UpdatePlannedDate_ResetDateWithEmpty_DateIsNull()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = userId});

            // act
            api.UpdatePlannedDate(token, 1, "12-12-2011");
            api.UpdatePlannedDate(token, 1, "");

            // assert
            var task = ApiTestsCommonSetup.SubmittedTasks[0];
            task.PlannedDate.Should().Be(null);
        }

        [Test]
        public void UpdatePlannedDate_ResetDateWithNull_DateIsNull()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = userId});

            // act
            api.UpdatePlannedDate(token, 1, "12-12-2011");
            api.UpdatePlannedDate(token, 1, null);

            // assert
            var task = ApiTestsCommonSetup.SubmittedTasks[0];
            task.PlannedDate.Should().Be(null);
        }

        [Test]
        public void DeleteAllDone_DeletesAllDoneTasks()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = userId });

            // act
            api.DeleteAllDone(token);
            
            // assert
            ApiTestsCommonSetup.DeletedTasks.Count.Should().Be(1);
        }

        [Test]
        public void UndoAll_AllDoneMadeActive()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = userId });

            // act
            api.UndoAll(token);

            // assert
            ApiTestsCommonSetup.AllTasks.Where(t => t.Done).Count().Should().Be(0);
        }

        [Test]
        public void Undo_DoneTaskUndo()
        {
            // arrange
            var userId = 100;
            var token = "4a891b4d0bb22f83482f9fb5bafeb4b8";
            var repository = ApiTestsCommonSetup.SetupMockRepository(userId);
            var date = new Mock<IDateTimeProviderService>();
            var service = new Mock<IApiService>();
            var shareService = new Mock<IShareService>();
            var api = new ApiV11Controller(service.Object, repository.Object, date.Object, shareService.Object);

            service.Setup(s => s.GetUserByApiToken(token)).Returns(new User { Id = userId });

            // act
            api.Undo(token, 7);

            // assert
            ApiTestsCommonSetup.AllTasks.Where(t => t.Id == 7).Single().Done.Should().Be.False();

        }
    }
}
