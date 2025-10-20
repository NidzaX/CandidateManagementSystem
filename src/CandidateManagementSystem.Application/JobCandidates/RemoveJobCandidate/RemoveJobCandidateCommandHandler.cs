using CandidateManagementSystem.Application.Abstractions.Messaging;
using CandidateManagementSystem.Application.JobCandidates.RemoveJobCandidate;
using CandidateManagementSystem.Domain.Abstractions;
using CandidateManagementSystem.Domain.JobCandidates;

namespace CandidateManagementSystem.Application.JobCandidates.RemoveCandidate;

internal sealed class RemoveCandidateCommandHandler : ICommandHandler<RemoveJobCandidateCommand, Guid>
{
    private readonly IJobCandidateRepository _jobCandidateRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveCandidateCommandHandler(
        IJobCandidateRepository jobCandidateRepository,
        IUnitOfWork unitOfWork)
    {
        _jobCandidateRepository = jobCandidateRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid>> Handle(RemoveJobCandidateCommand request, CancellationToken cancellationToken)
    {
        JobCandidate jobCandidate = await _jobCandidateRepository.GetByIdAsync(request.CandidateId, cancellationToken);
        if (jobCandidate is null)
        {
            return Result.Failure<Guid>(JobCandidateErrors.NotFound);
        }

        _jobCandidateRepository.Remove(jobCandidate);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success(jobCandidate.Id);
    }
}