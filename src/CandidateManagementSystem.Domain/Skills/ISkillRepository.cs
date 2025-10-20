namespace CandidateManagementSystem.Domain.Skills;

public interface ISkillRepository
{
    Task<Skill?> GetByIdAsync(Guid id,  CancellationToken cancellationToken);
    
    Task<List<Skill>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken);
    
    void Add(Skill skill);
}