using CandidateManagementSystem.Domain.Abstractions;

namespace CandidateManagementSystem.Domain.Skills.Events;

public sealed record SkillCreatedDomainEvent(Guid SkillId) : IDomainEvent;