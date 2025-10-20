using CandidateManagementSystem.Domain.Abstractions;

namespace CandidateManagementSystem.Domain.Skills;

public class SkillErrors
{
    public static Error NotFound = new(
        "Skill.NotFound",
        "The skill with the specified identifier was not found.");
    
    public static Error NameEmpty = new(
        "Skill.NameEmpty", 
        "Skill name cannot be empty");

    public static Error NameTooLong = new(
        "Skill.NameTooLong",
        "Skill name cannot exceed 200 characters");

    public static Error DuplicateName = new(
        "Skill.DuplicateName",
        "A skill with this name already exists");

    public static Error InvalidName = new(
        "Skill.InvalidName",
        "The provided skill name is invalid");
}