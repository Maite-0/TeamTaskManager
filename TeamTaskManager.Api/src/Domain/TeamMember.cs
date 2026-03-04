namespace TeamTaskManager.Domain
{

    public class TeamMember
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public List<TaskItem> Tasks { get; set; } = new();
    }
}
