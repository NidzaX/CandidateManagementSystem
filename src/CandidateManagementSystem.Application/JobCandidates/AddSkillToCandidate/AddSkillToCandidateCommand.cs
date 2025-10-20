using CandidateManagementSystem.Application.Abstractions.Messaging;

namespace CandidateManagementSystem.Application.JobCandidates.AddSkillToCandidate;

public sealed record AddSkillToCandidateCommand(
    Guid JobCandidateId,
    Guid SkillId) : ICommand<Guid>;