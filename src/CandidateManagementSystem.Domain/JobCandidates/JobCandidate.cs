using CandidateManagementSystem.Domain.Abstractions;
using CandidateManagementSystem.Domain.JobCandidates.Events;
using CandidateManagementSystem.Domain.Skills;

namespace CandidateManagementSystem.Domain.JobCandidates;

public class JobCandidate : Entity
{
    private JobCandidate(Guid id, FirstName firstName, LastName lastName, DateTime birth, ContactNumber contactNumber, Email email)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Birth = birth;
        ContactNumber = contactNumber;
        Email = email;
    }

    private JobCandidate()
    {
    }
    
    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public DateTime Birth { get; private set; }
    public ContactNumber ContactNumber { get; private set; }
    public Email Email { get; private set; }

    public HashSet<Skill> Skills { get; private set; } = new();
    

    public static JobCandidate Create(FirstName firstName, LastName lastName, DateTime birth,
        ContactNumber contactNumber, Email email)
    {
        JobCandidate jobCandidate = new(Guid.NewGuid(), firstName, lastName, birth, contactNumber, email);
        
        jobCandidate.RaiseDomainEvents(new JobCandidateCreatedDomainEvent(jobCandidate.Id));
        
        return jobCandidate;
    }
}