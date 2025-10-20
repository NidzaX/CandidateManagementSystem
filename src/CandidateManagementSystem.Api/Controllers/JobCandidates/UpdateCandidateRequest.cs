namespace CandidateManagementSystem.Api.Controllers.JobCandidates;

public record UpdateCandidateRequest(
    string FirstName,
    string LastName,
    string Birth,
    string ContactNumber,
    string Email,
    List<Guid> SkillIds);