using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trackyt.Core.DAL.DataModel;
using AutoMapper;
using Moq;
using Trackyt.Core.DAL.Repositories;
using Web.Infrastructure;

namespace Trackyt.Core.Tests.Tests.Controllers.API
{
    class ApiTestsCommonSetup
    {
        public static IList<Task> SubmittedTasks = new List<Task>();
        public static IList<Task> DeletedTasks = new List<Task>();
        public static DateTime StartedDate = new DateTime(2010, 12, 29, 12, 12, 12);
        public static List<Task> AllTasks;

        public static IMappingEngine SetupMapper()
        {
            TrackyMapping.SetupMapping();

            return Mapper.Engine;
        }

        public static Mock<ITasksRepository> SetupMockRepository(int userId)
        {
            var repository = new Mock<ITasksRepository>();
            AllTasks = new List<Task>()
                {
                    new Task { Id = 1, Description = "Task1", ActualWork = 0, Number = 1, Status = 0, UserId = userId },
                    new Task { Id = 2, Description = "Task2", ActualWork = 15, StartedDate = StartedDate, Number = 2, Status = 1, UserId = userId },
                    new Task { Id = 3, Description = "Task3", ActualWork = 20, Number = 3, Status = 2, UserId = userId },
                    new Task { Id = 4, Description = "Task3", ActualWork = 0, Number = 4, UserId = userId + 1 },
                    new Task { Id = 5, Description = "Task3", ActualWork = 7, Number = 1, UserId = userId + 2 },
                    new Task { Id = 6, Description = "Task3", ActualWork = 4, Number = 1, UserId = userId + 3 },
                    new Task { Id = 7, Description = "DoneTask1", UserId = userId, Done = true }
                };
            repository.Setup(f => f.Tasks).Returns(AllTasks.AsQueryable());

            var index = 222;
            SubmittedTasks.Clear();
            DeletedTasks.Clear();
            repository.Setup(f => f.Save(It.IsAny<Task>())).Callback((Task t) =>
            {
                if (t.Id == 0)
                {
                    t.Id = index++;
                }
                SubmittedTasks.Add(t);
            }
            );
            repository.Setup(f => f.Delete(It.IsAny<Task>())).Callback((Task t) =>
            {
                DeletedTasks.Add(t);
            }
            );

            return repository;
        }
    }
}
