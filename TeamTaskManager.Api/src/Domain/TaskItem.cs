namespace TeamTaskManager.Domain
{

    public class TaskItem
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public TaskStatus Status { get; set; }

        public TaskPriority Priority { get; set; }

        public List<TeamMember> Assignees { get; set; } = new();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
