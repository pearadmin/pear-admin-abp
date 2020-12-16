using PearAdmin.AbpTemplate.TaskCenter.DailyTasks;
using Shouldly;
using Xunit;

namespace PearAdmin.AbpTemplate.Core.Tests
{
    public class DailyTaskWorkFlow_UnitTest
    {
        [Fact]
        public void Create()
        {
            // Arrange
            var name = "test";

            // Act
            var dailyTask = DailyTask.Create(name);

            // Assert
            dailyTask.ShouldNotBeNull();
        }

        [Fact]
        public void Progress()
        {
            // Arrange
            var name = "test";
            var dailyTask = DailyTask.Create(name);

            // Act
            dailyTask.Progress();

            // Assert
            dailyTask.TaskState.Id.ShouldBe(TaskStateType.Progressing.Id);
        }
    }
}
