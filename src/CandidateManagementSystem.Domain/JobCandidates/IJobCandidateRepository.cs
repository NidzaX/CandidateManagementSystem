namespace CandidateManagementSystem.Domain.JobCandidates;

public interface IJobCandidateRepository
{
    Task<JobCandidate?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<List<JobCandidate>> SearchAsync(string? name, List<string>? skills, CancellationToken cancellationToken);
    
    void Add(JobCandidate jobCandidate);

    void Remove(JobCandidate jobCandidate);
}