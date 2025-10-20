using CandidateManagementSystem.Domain.Skills;

namespace CandidateManagementSystem.Domain.UnitTests.Skills;

internal static class SkillData
{
    public static Name ValidName => new("c#");
    public static Name AnotherValidName => new("javascript");
    public static Name NameWithSpecialCharacters => new("c++");
    public static Name LongName => new("asp.net core web api development");
    
    public static Name CreateName(string name) => new(name.ToLowerInvariant());

}