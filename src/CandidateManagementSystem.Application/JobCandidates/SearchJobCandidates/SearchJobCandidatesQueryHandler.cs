using CandidateManagementSystem.Application.Abstractions.Messaging;
using CandidateManagementSystem.Domain.Abstractions;
using CandidateManagementSystem.Domain.JobCandidates;

namespace CandidateManagementSystem.Application.JobCandidates.SearchJobCandidates;

internal sealed class SearchJobCandidatesQueryHandler : IQueryHandler<SearchJobCandidatesQuery, List<JobCandidate>>
{
    private readonly IJobCandidateRepository _jobCandidateRepository;

    public SearchJobCandidatesQueryHandler(IJobCandidateRepository jobCandidateRepository)
    {
        _jobCandidateRepository = jobCandidateRepository;
    }
    
    public async Task<Result<List<JobCandidate>>> Handle(SearchJobCandidatesQuery query, CancellationToken cancellationToken)
    {
        List<JobCandidate> jobCandidates = await _jobCandidateRepository.SearchAsync(query.Name, query.Skills, cancellationToken);

        return  Result.Success(jobCandidates);
    }
}