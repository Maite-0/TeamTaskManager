using TeamTaskManager.Domain;

public interface ITeamMemberRepository
{
    Task<IEnumerable<TeamMember>> GetAllAsync();
    Task<TeamMember?> GetByIdAsync(Guid id);
    Task<IEnumerable<TeamMember>> GetByIdsAsync(List<Guid> ids);
    Task AddAsync(TeamMember member);
    Task SaveChangesAsync();
}