using TeamTaskManager.Domain;
using TaskStatus = TeamTaskManager.Domain.TaskStatus;

namespace TeamTaskManager.Dtos
{
    public class UpdateTaskDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
    }
}

