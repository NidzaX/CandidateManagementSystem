using CandidateManagementSystem.Application.Abstractions.Messaging;
using CandidateManagementSystem.Domain.Abstractions;
using CandidateManagementSystem.Domain.Skills;

namespace CandidateManagementSystem.Application.Skills.AddSkill;

internal sealed class AddSkillCommandHandler : ICommandHandler<AddSkillCommand, Guid>
{
    private readonly ISkillRepository _skillRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddSkillCommandHandler(
        ISkillRepository skillRepository,
        IUnitOfWork unitOfWork)
    {
        _skillRepository = skillRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<Guid>> Handle(AddSkillCommand request, CancellationToken cancellationToken)
    {
        Skill skill = Skill.Create(
            new Name(request.Name));
        
        _skillRepository.Add(skill);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return skill.Id;
    }
}