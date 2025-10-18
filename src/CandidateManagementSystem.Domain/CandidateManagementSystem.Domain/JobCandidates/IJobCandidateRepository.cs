namespace CandidateManagementSystem.Domain.JobCandidates;

public interface IJobCandidateRepository
{
    Task<JobCandidate?> GetbyIdAsync(Guid id, CancellationToken cancellationToken);
    
    void Add(JobCandidate jobCandidate);
}