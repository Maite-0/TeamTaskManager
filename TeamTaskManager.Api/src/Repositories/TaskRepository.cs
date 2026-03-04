using Microsoft.EntityFrameworkCore;
using TeamTaskManager.Domain;
using TeamTaskManager.Infrastructure;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskItem>> GetTasksAsync(TaskQueryParameters parameters)
    {
        var query = _context.Tasks
            .Include(t => t.Assignees)
            .AsQueryable();

        // Search by title
        if (!string.IsNullOrWhiteSpace(parameters.Search))
            query = query.Where(t => t.Title.Contains(parameters.Search));

        // Sorting
        query = parameters.SortBy?.ToLower() switch
        {
            "title" => parameters.Descending
                ? query.OrderByDescending(t => t.Title)
                : query.OrderBy(t => t.Title),

            "priority" => parameters.Descending
                ? query.OrderByDescending(t => t.Priority)
                : query.OrderBy(t => t.Priority),

            _ => parameters.Descending
                ? query.OrderByDescending(t => t.CreatedAt)
                : query.OrderBy(t => t.CreatedAt)
        };

        // Pagination
        return await query
            .Skip((parameters.PageNumber - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id)
    {
        return await _context.Tasks
            .Include(t => t.Assignees)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task AddAsync(TaskItem task)
        => await _context.Tasks.AddAsync(task);

    public void Update(TaskItem task)
        => _context.Tasks.Update(task);

    public void Delete(TaskItem task)
        => _context.Tasks.Remove(task);

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}