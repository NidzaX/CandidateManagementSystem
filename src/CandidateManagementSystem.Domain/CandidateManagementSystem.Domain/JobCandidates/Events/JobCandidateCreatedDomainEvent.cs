using CandidateManagementSystem.Domain.Abstractions;

namespace CandidateManagementSystem.Domain.JobCandidates.Events;

public record JobCandidateCreatedDomainEvent(Guid JobCandidateId) : IDomainEvent;