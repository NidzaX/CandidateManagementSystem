using FluentValidation;

namespace CandidateManagementSystem.Application.JobCandidates.AddJobCandidate;

internal sealed class AddJobCandidateCommandValidator : AbstractValidator<AddJobCandidateCommand>
{
    public AddJobCandidateCommandValidator()
    {
        RuleFor(c => c.FirstName).NotEmpty();
        
        RuleFor(c => c.LastName).NotEmpty();

        RuleFor(c => c.Email).EmailAddress();
        
        RuleFor(c => c.ContactNumber ).NotEmpty();
    }
}