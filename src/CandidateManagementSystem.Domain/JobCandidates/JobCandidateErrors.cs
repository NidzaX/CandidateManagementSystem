using CandidateManagementSystem.Domain.Abstractions;

namespace CandidateManagementSystem.Domain.JobCandidates;

public static class JobCandidateErrors
{
    public static Error NotFound = new(
        "JobCandidate.Found",
        "The job candidate with the specified identifier was not found.");

    public static Error NotAssociated = new(
        "JobCandidate.NotAssociated",
        "This skill is not associated with the candidate");

    public static Error SkillsRequired = new(
        "JobCandidate.SkillsRequired",
        "Candidate must have at least one skill");
    
    public static Error SkillAlreadyAdded = new(
        "JobCandidate.SkillAlreadyAdded",
        "This skill is already associated with the candidate");
    
    
}