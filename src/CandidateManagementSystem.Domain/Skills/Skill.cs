using CandidateManagementSystem.Domain.Abstractions;
using CandidateManagementSystem.Domain.Skills.Events;

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

    public static Skill Create(Name name)
    {
        Skill skill = new(Guid.NewGuid(), name);
        
        skill.RaiseDomainEvents(new SkillCreatedDomainEvent(skill.Id));

        return skill;
    }
    
    public Name Name { get; private set; }

    public override bool Equals(object obj)
    {
        return obj is Skill skill && Name.Equals(skill.Name);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}