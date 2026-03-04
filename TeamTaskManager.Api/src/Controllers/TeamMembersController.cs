using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeamTaskManager.Domain;
using TeamTaskManager.Dtos;


namespace TeamTaskManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamMembersController : ControllerBase
{
    private readonly ITeamMemberRepository _repository;
    private readonly IMapper _mapper;

    public TeamMembersController(
        ITeamMemberRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    // GET: api/teammembers
    [HttpGet]
    public async Task<IActionResult> GetMembers()
    {
        var members = await _repository.GetAllAsync();
        var result = _mapper.Map<IEnumerable<TeamMemberResponseDto>>(members);
        return Ok(result);
    }

    // GET: api/teammembers/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMember(Guid id)
    {
        var member = await _repository.GetByIdAsync(id);

        if (member == null)
            return NotFound("Team member not found");
        var result = _mapper.Map<TeamMemberResponseDto>(member);

        return Ok(result);
    }

    // POST: api/teammembers
    [HttpPost]
    public async Task<IActionResult> CreateMember(CreateTeamMemberDto dto)
    {
        var member = _mapper.Map<TeamMember>(dto);
        member.Id = Guid.NewGuid();
        await _repository.AddAsync(member);
        await _repository.SaveChangesAsync();
        var result = _mapper.Map<TeamMemberResponseDto>(member);
        return CreatedAtAction(nameof(GetMember),
            new { id = member.Id },
            result);
    }
}