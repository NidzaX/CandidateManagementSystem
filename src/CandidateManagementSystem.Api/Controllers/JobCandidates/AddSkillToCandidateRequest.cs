namespace CandidateManagementSystem.Api.Controllers.JobCandidates;

public record AddSkillToCandidateRequest(
    Guid JobCandidateId,
    Guid SkillId);