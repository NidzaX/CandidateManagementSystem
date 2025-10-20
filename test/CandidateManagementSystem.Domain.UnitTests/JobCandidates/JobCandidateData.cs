using CandidateManagementSystem.Domain.JobCandidates;
using CandidateManagementSystem.Domain.Skills;

namespace CandidateManagementSystem.Domain.UnitTests.JobCandidates;

internal static class JobCandidateData
{
    public static FirstName FirstName => new("First");
    public static LastName LastName => new("Last");
    public static DateTime Birth => new(1990, 1, 1);
    public static ContactNumber ContactNumber => new("+1234567890");
    public static Email Email => new("test@test.com");
    
    public static FirstName DifferentFirstName => new("DifferentFirst");
    public static LastName DifferentLastName => new("DifferentLast");
    public static DateTime DifferentBirth => new(1985, 5, 15);
    public static ContactNumber DifferentContactNumber => new("+9876543210");
    public static Email DifferentEmail => new("different@test.com");

    public static List<Skill> SampleSkills => new()
    {
        Skill.Create(new Name("C#")),
        Skill.Create(new Name("SQL")),
        Skill.Create(new Name("JavaScript"))
    };

    public static List<Skill> DifferentSkills => new()
    {
        Skill.Create(new Name("Python")),
        Skill.Create(new Name("Azure")),
        Skill.Create(new Name("Docker"))
    };
    
    public static Skill CreateSkill(string skillName)
    {
        return Skill.Create(new Name(skillName));
    }
}