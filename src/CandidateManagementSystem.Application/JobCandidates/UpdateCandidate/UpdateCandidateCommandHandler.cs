using CandidateManagementSystem.Application.Abstractions.Messaging;
using CandidateManagementSystem.Domain.Abstractions;
using CandidateManagementSystem.Domain.JobCandidates;
using CandidateManagementSystem.Domain.Skills;

namespace CandidateManagementSystem.Application.JobCandidates.UpdateCandidate;

internal sealed class UpdateCandidateCommandHandler : ICommandHandler<UpdateCandidateCommand, Guid>
{
    private readonly IJobCandidateRepository _jobCandidateRepository;
    private readonly ISkillRepository _skillRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateCandidateCommandHandler(
        IJobCandidateRepository jobCandidateRepository,
        ISkillRepository skillRepository,
        IUnitOfWork unitOfWork)
    {
        _jobCandidateRepository = jobCandidateRepository;
        _skillRepository = skillRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid>> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
    {
        if (request.SkillIds == null || !request.SkillIds.Any())
        {
            return Result.Failure<Guid>(JobCandidateErrors.SkillsRequired);
        }
        
        List<Guid> duplicateSkillIds = request.SkillIds
            .GroupBy(skillId => skillId)
            .Where(group => group.Count() > 1)
            .Select(group => group.Key)
            .ToList();
        
        if (duplicateSkillIds.Any())
        {
            return Result.Failure<Guid>(JobCandidateErrors.SkillAlreadyAdded);
        }
        
        JobCandidate jobCandidate = await _jobCandidateRepository.GetByIdAsync(request.CandidateId, cancellationToken);

        if (jobCandidate == null)
        {
            return Result.Failure<Guid>(JobCandidateErrors.NotFound);
        }

        jobCandidate.UpdatePersonalInfo(
            new FirstName(request.FirstName),
            new LastName(request.LastName),
            request.Birth,
            new ContactNumber(request.ContactNumber),
            new Email(request.Email));
        
        List<Skill> skills = await _skillRepository.GetByIdsAsync(request.SkillIds, cancellationToken);
        jobCandidate.UpdateSkills(skills);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success(jobCandidate.Id);
    }
}