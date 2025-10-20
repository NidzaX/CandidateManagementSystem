using CandidateManagementSystem.Application.Abstractions.Messaging;
using CandidateManagementSystem.Domain.Abstractions;
using CandidateManagementSystem.Domain.JobCandidates;
using CandidateManagementSystem.Domain.Skills;

namespace CandidateManagementSystem.Application.JobCandidates.RemoveSkillFromCandidate;

internal sealed class RemoveSkillFromCandidateCommandHandler : ICommandHandler<RemoveSkillFromCandidateCommand, Guid>
{
    private readonly IJobCandidateRepository _jobCandidateRepository;
    private readonly ISkillRepository _skillRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveSkillFromCandidateCommandHandler(
        IJobCandidateRepository jobCandidateRepository,
        ISkillRepository skillRepository,
        IUnitOfWork unitOfWork)
    {
        _jobCandidateRepository = jobCandidateRepository;
        _skillRepository = skillRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid>> Handle(RemoveSkillFromCandidateCommand request, CancellationToken cancellationToken)
    {
        JobCandidate jobCandidate = await _jobCandidateRepository.GetByIdAsync(request.JobCandidateId, cancellationToken);
        if (jobCandidate == null)
        {
            return Result.Failure<Guid>(JobCandidateErrors.NotFound);
        }
        
        Skill skill = await _skillRepository.GetByIdAsync(request.SkillId, cancellationToken);
        if (skill == null)
        {
            return Result.Failure<Guid>(SkillErrors.NotFound);
        }
        
        if (!jobCandidate.Skills.Contains(skill))
        {
            return Result.Failure<Guid>(JobCandidateErrors.NotAssociated);
        }
        
        jobCandidate.Skills.Remove(skill);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success(jobCandidate.Id);
    }
}