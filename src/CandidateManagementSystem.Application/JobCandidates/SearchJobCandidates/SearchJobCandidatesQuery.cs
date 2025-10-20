using CandidateManagementSystem.Application.Abstractions.Messaging;
using CandidateManagementSystem.Domain.JobCandidates;

namespace CandidateManagementSystem.Application.JobCandidates.SearchJobCandidates;

public sealed record SearchJobCandidatesQuery(
    string? Name,
    List<string>? Skills) : IQuery<List<JobCandidate>>;