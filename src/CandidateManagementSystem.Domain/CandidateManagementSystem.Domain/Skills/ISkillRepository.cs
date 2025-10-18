namespace CandidateManagementSystem.Domain.Skills;

public interface ISkillRepository
{
    Task<Skill?> GetByIdAsync(Guid id,  CancellationToken cancellationToken);
}