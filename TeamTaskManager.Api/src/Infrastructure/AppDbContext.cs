using Microsoft.EntityFrameworkCore;
using TeamTaskManager.Domain;

namespace TeamTaskManager.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskItem> Tasks => Set<TaskItem>();
        public DbSet<TeamMember> TeamMembers => Set<TeamMember>();
    }
}
