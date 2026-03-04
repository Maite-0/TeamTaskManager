using Moq;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using TeamTaskManager.Controllers;
using TeamTaskManager.Domain;
using TeamTaskManager.Dtos;
using TaskStatus = TeamTaskManager.Domain.TaskStatus;

public class TasksControllerTests
{
    [Fact]
    public async Task UpdateTask_ReturnsNoContent_WhenSuccessful()
    {
        // Arrange
        var taskId = Guid.NewGuid();

        var existingTask = new TaskItem
        {
            Id = taskId,
            Title = "Old Title",
            Description = "Old Description"
        };

        var mockTaskRepo = new Mock<ITaskRepository>();
        mockTaskRepo.Setup(r => r.GetByIdAsync(taskId))
                    .ReturnsAsync(existingTask);

        var mockTeamMemberRepo = new Mock<ITeamMemberRepository>();
        // Not needed for this test, but MUST be provided

        var mockMapper = new Mock<IMapper>();
        mockMapper.Setup(m => m.Map(It.IsAny<UpdateTaskDto>(), existingTask));

        var controller = new TasksController(
            mockTaskRepo.Object,
            mockTeamMemberRepo.Object,  
            mockMapper.Object);

        var dto = new UpdateTaskDto
        {
            Title = "New Title",
            Description = "New Description",
            Status = TaskStatus.InProgress,
            Priority = TaskPriority.High
        };

        // Act
        var result = await controller.UpdateTask(taskId, dto);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}