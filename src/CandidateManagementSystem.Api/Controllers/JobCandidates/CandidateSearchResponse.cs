namespace CandidateManagementSystem.Api.Controllers.JobCandidates;

public record CandidateSearchResponse(
    Guid Id,
    string FirstName,
    string LastName,
    DateTime Birth,
    string ContactNumber,
    string Email,
    List<SkillResponse> Skills);