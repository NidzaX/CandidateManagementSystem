using CandidateManagementSystem.Domain.Abstractions;

namespace CandidateManagementSystem.Domain.Skills;

public class SkillErrors
{
    public static Error NotFound = new(
        "Skill.NotFound",
        "The skill with the specified identifier was not found.");
}