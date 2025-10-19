namespace CandidateManagementSystem.Api.Controllers.JobCandidates;

public record AddJobCandidateRequest(
    string FirstName,
    string LastName,
    string Birth,
    string ContactNumber,
    string Email);