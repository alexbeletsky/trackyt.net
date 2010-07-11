using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Trackyourtasks.Core.BLL.Tasks;
using Trackyourtasks.Core.Tests.Mocks;
using Trackyourtasks.Core.DAL.DataModel;

namespace Trackyourtasks.Core.Tests.BLL
{
    [TestFixture]
    public class AddTaskTests
    {
        [Test]
        public void AddTask()
        {
            //INIT
            var resository = new TasksRepositoryMock();
            var addTask = new AddTask(resository);

            //ACT
            var task = new Task()
                {
                    Number = 0,
                    Description = "Task to be added",
                    UserId = 10,
                    ActualWork = new DateTimeOffset()
                };

            addTask.Add(task);

            //POST
            var foundTask = resository.FindTaskByUserId(task.UserId);
            Assert.That(foundTask, Is.Not.Null);
        }
    }
}
