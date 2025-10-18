using CandidateManagementSystem.Domain.Abstractions;

namespace CandidateManagementSystem.Domain.JobCandidates;

public static class JobCandidateErrors
{
    public static Error NotFound = new(
        "JobCandidate.Found",
        "The job candidate with the specified identifier was not found.");

    public static Error InvalidCredentials = new(
        "JobCandidate.InvalidCredentials",
        "The provided credentials were invalid");
}