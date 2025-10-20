using CandidateManagementSystem.Application.Abstractions.Messaging;

namespace CandidateManagementSystem.Application.JobCandidates.RemoveSkillFromCandidate;

public sealed record RemoveSkillFromCandidateCommand(
    Guid JobCandidateId,
    Guid SkillId) : ICommand<Guid>;
