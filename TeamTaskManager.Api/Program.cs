using Microsoft.EntityFrameworkCore;
using TeamTaskManager.Domain;
using TeamTaskManager.Infrastructure;
using TeamTaskManager.Middleware;
using TaskStatus = TeamTaskManager.Domain.TaskStatus;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TaskDb"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

//SEEDING DATA 
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.TeamMembers.Any())
    {
        // Create team members
        var member1 = new TeamMember
        {
            Id = Guid.NewGuid(),
            Name = "Alice Johnson",
            Email = "alice.johnson@example.com"
        };

        var member2 = new TeamMember
        {
            Id = Guid.NewGuid(),
            Name = "Bob Smith",
            Email = "bob.smith@example.com"
        };

        var member3 = new TeamMember
        {
            Id = Guid.NewGuid(),
            Name = "Bob Smith",
            Email = "bob.smith@example.com"
        };

        context.TeamMembers.AddRange(member1, member2);

        // Create tasks and assign them
        var task1 = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = "Design Database Schema",
            Description = "Create entities and relationships",
            Priority = TaskPriority.High,
            Status = TaskStatus.Todo,
            Assignees = [member3, member2],
            CreatedAt = DateTime.UtcNow
        };

        var task2 = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = "Implement Repository Pattern",
            Description = "Refactor controllers",
            Priority = TaskPriority.Medium,
            Status = TaskStatus.InProgress,
            Assignees = [member1, member2],
            CreatedAt = DateTime.UtcNow
        };

        var task3 = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = "Write Documentation",
            Description = "Prepare README and solution.md",
            Priority = TaskPriority.Low,
            Status = TaskStatus.Todo,
            Assignees = [member3],
            CreatedAt = DateTime.UtcNow
        };

        context.Tasks.AddRange(task1, task2, task3);

        context.SaveChanges();
    }
}
app.Run();
