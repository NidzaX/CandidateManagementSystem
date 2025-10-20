using CandidateManagementSystem.Application.Abstractions.Messaging;

namespace CandidateManagementSystem.Application.Skills.AddSkill;

public sealed record AddSkillCommand(
    string Name) : ICommand<Guid>;