using CandidateManagementSystem.Application.Abstractions.Messaging;

namespace CandidateManagementSystem.Application.JobCandidates.RemoveJobCandidate;

public sealed record RemoveJobCandidateCommand(
    Guid CandidateId) : ICommand<Guid>;
