namespace CandidateManagementSystem.Domain.JobCandidates;

public interface IJobCandidateRepository
{
    Task<JobCandidate?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    void Add(JobCandidate jobCandidate);
}