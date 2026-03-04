using System.ComponentModel.DataAnnotations;

namespace TeamTaskManager.Dtos
{
    public class AssignTaskDto
    {
        [Required]
        public List<Guid> TeamMemberIds { get; set; } = new();
    }
}

