using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeamTaskManager.Domain;
using TeamTaskManager.Dtos;
using TaskStatus = TeamTaskManager.Domain.TaskStatus;

namespace TeamTaskManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskRepository _repository;
    private readonly ITeamMemberRepository _teamMemberRepository;
    private readonly IMapper _mapper;

    public TasksController(ITaskRepository repository, ITeamMemberRepository teamMemberRrepository, IMapper mapper)
    {
        _repository = repository;
        _teamMemberRepository = teamMemberRrepository;
        _mapper = mapper;
    }

    // GET: api/tasks
    [HttpGet]
    public async Task<IActionResult> GetTasks([FromQuery] TaskQueryParameters parameters)
    {
        var tasks = await _repository.GetTasksAsync(parameters);
        var result = _mapper.Map<IEnumerable<TaskResponseDto>>(tasks);
        return Ok(result);
    }

    // GET: api/tasks/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(Guid id)
    {
        var task = await _repository.GetByIdAsync(id);
        if (task == null)
            return NotFound("Task not found");
        var result = _mapper.Map<TaskResponseDto>(task);
        return Ok(result);
    }

    // POST: api/tasks
    [HttpPost]
    public async Task<IActionResult> CreateTask(CreateTaskDto dto)
    {
        var task = _mapper.Map<TaskItem>(dto);
        task.Id = Guid.NewGuid();
        task.Status = TaskStatus.Todo;
        await _repository.AddAsync(task);
        await _repository.SaveChangesAsync();
        var result = _mapper.Map<TaskResponseDto>(task);
        return CreatedAtAction(nameof(GetTask), new { id = task.Id }, result);
    }

    // PUT: api/tasks/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(Guid id, UpdateTaskDto dto)
    {
        var task = await _repository.GetByIdAsync(id);
        if (task == null)
            return NotFound("Task not found");
        _mapper.Map(dto, task);
        _repository.Update(task);
        await _repository.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/tasks/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        var task = await _repository.GetByIdAsync(id);

        if (task == null)
            return NotFound("Task not found");
        _repository.Delete(task);

        await _repository.SaveChangesAsync();
        return NoContent();
    }


    [HttpPost("{id}/assign")]
    public async Task<IActionResult> AssignTask(Guid id, AssignTaskDto dto)
    {
        var task = await _repository.GetByIdAsync(id);
        if (task == null)
            return NotFound("Task not found");
        var members = await _teamMemberRepository.GetByIdsAsync(dto.TeamMemberIds);

        if (!members.Any())
            return NotFound("No valid team members found");
        task.Assignees = members.ToList();

        _repository.Update(task);
        await _repository.SaveChangesAsync();
        var result = _mapper.Map<TaskResponseDto>(task);
        return Ok(result);
    }
}