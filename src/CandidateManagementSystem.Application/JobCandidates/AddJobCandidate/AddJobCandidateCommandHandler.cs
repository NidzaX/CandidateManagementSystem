using CandidateManagementSystem.Application.Abstractions.Messaging;
using CandidateManagementSystem.Domain.Abstractions;
using CandidateManagementSystem.Domain.JobCandidates;

namespace CandidateManagementSystem.Application.JobCandidates.AddJobCandidate;

internal sealed class AddJobCandidateCommandHandler : ICommandHandler<AddJobCandidateCommand, Guid>
{
    private readonly IJobCandidateRepository _jobCandidateRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddJobCandidateCommandHandler(
        IJobCandidateRepository jobCandidateRepository,
        IUnitOfWork unitOfWork)
    {
        _jobCandidateRepository = jobCandidateRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid>> Handle(AddJobCandidateCommand request, CancellationToken cancellationToken)
    {
        JobCandidate jobCandidate = JobCandidate.Create(
            new FirstName(request.FirstName),
            new LastName(request.LastName),
            request.Birth,
            new ContactNumber(request.ContactNumber),
            new Email(request.Email));
        
        _jobCandidateRepository.Add(jobCandidate);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return jobCandidate.Id;
    }
}