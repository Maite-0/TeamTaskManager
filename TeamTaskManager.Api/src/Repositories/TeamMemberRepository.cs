using Microsoft.EntityFrameworkCore;
using TeamTaskManager.Domain;
using TeamTaskManager.Infrastructure;

public class TeamMemberRepository : ITeamMemberRepository
{
    private readonly AppDbContext _context;

    public TeamMemberRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TeamMember>> GetAllAsync()
    {
        return await _context.TeamMembers.ToListAsync();
    }

    public async Task<TeamMember?> GetByIdAsync(Guid id)
    {
        return await _context.TeamMembers.FindAsync(id);
    }
    public async Task<IEnumerable<TeamMember>> GetByIdsAsync(List<Guid> ids)
    {
        return await _context.TeamMembers
            .Where(m => ids.Contains(m.Id))
            .ToListAsync();
    }
    public async Task AddAsync(TeamMember member)
    {
        await _context.TeamMembers.AddAsync(member);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}