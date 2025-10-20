using CandidateManagementSystem.Domain.Abstractions;

namespace CandidateManagementSystem.Domain.JobCandidates.Events;

public sealed record JobCandidateCreatedDomainEvent(Guid JobCandidateId) : IDomainEvent;