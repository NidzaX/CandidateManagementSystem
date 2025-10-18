using System.Runtime.InteropServices.JavaScript;
using CandidateManagementSystem.Application.Abstractions.Messaging;

namespace CandidateManagementSystem.Application.JobCandidates.AddJobCandidate;

public sealed record AddJobCandidateCommand(
    string FirstName,
    string LastName,
    DateTime Birth,
    string ContactNumber,
    string Email) : ICommand<Guid>;
