using TeamTaskManager.Domain;

public interface ITaskRepository
{
    Task<IEnumerable<TaskItem>> GetTasksAsync(TaskQueryParameters parameters);
    Task<TaskItem?> GetByIdAsync(Guid id);
    Task AddAsync(TaskItem task);
    void Update(TaskItem task);
    void Delete(TaskItem task);
    Task SaveChangesAsync();
}