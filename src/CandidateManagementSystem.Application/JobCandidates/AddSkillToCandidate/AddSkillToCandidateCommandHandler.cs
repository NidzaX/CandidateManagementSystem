using CandidateManagementSystem.Application.Abstractions.Messaging;
using CandidateManagementSystem.Domain.Abstractions;
using CandidateManagementSystem.Domain.JobCandidates;
using CandidateManagementSystem.Domain.Skills;

namespace CandidateManagementSystem.Application.JobCandidates.AddSkillToCandidate;

internal sealed class AddSkillToCandidateCommandHandler : ICommandHandler<AddSkillToCandidateCommand, Guid>
{
    private readonly IJobCandidateRepository _jobCandidateRepository;
    private readonly ISkillRepository _skillRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddSkillToCandidateCommandHandler(
        IJobCandidateRepository jobCandidateRepository,
        ISkillRepository skillRepository,
        IUnitOfWork unitOfWork)
    {
        _jobCandidateRepository = jobCandidateRepository;
        _skillRepository = skillRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid>> Handle(AddSkillToCandidateCommand request, CancellationToken cancellationToken)
    {
        JobCandidate jobCandidate = await _jobCandidateRepository.GetByIdAsync(request.JobCandidateId, cancellationToken);
        if (jobCandidate is null)
        {
            return Result.Failure<Guid>(JobCandidateErrors.NotFound);
        }
        
        Skill skill = await _skillRepository.GetByIdAsync(request.SkillId, cancellationToken);
        if (skill is null)
        {
            return Result.Failure<Guid>(SkillErrors.NotFound);
        }
        
        if (jobCandidate.Skills.Any(s => s.Id == skill.Id))
        {
            return Result.Failure<Guid>(JobCandidateErrors.SkillAlreadyAdded);
        }
        
        jobCandidate.Skills.Add(skill);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success(jobCandidate.Id);
    }
}