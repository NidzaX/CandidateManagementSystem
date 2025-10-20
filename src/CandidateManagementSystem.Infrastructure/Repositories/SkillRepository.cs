using CandidateManagementSystem.Domain.Skills;
using Microsoft.EntityFrameworkCore;

namespace CandidateManagementSystem.Infrastructure.Repositories;

internal sealed class SkillRepository : Repository<Skill>, ISkillRepository
{
    public SkillRepository(ApplicationDbContext dbContext) 
        : base(dbContext)
    {
    }

    public async Task<List<Skill>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken)
    {
        return await DbContext.Set<Skill>()
            .Where(s => ids.Contains(s.Id))
            .ToListAsync(cancellationToken);
    }
}