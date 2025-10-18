using CandidateManagementSystem.Domain.Skills;

namespace CandidateManagementSystem.Infrastructure.Repositories;

internal sealed class SkillRepository : Repository<Skill>, ISkillRepository
{
    public SkillRepository(ApplicationDbContext dbContext) 
        : base(dbContext)
    {
    }
}