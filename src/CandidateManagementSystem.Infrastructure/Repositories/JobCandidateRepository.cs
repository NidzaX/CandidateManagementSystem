using CandidateManagementSystem.Domain.JobCandidates;

namespace CandidateManagementSystem.Infrastructure.Repositories;

internal sealed class JobCandidateRepository : Repository<JobCandidate>, IJobCandidateRepository
{
    public JobCandidateRepository(ApplicationDbContext dbContext) 
        : base(dbContext)
    {
    }
}