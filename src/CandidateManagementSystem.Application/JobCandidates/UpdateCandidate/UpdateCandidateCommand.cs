using CandidateManagementSystem.Application.Abstractions.Messaging;
using CandidateManagementSystem.Domain.JobCandidates;

namespace CandidateManagementSystem.Application.JobCandidates.UpdateCandidate;

public sealed record UpdateCandidateCommand(
    Guid CandidateId,
    string FirstName,
    string LastName,
    DateTime Birth,
    string ContactNumber,
    string Email,
    List<Guid> SkillIds) : ICommand<Guid>;