using CandidateManagementSystem.Domain.Abstractions;

namespace CandidateManagementSystem.Domain.Skills;

public class Skill : Entity
{
    private Skill(Guid id, Name name) : base(id)
    {
        Name = name;
    }

    private Skill()
    {
    }
    
    public Name Name { get; set; }

    public override bool Equals(object obj)
    {
        return obj is Skill skill && Name.Equals(skill.Name);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}