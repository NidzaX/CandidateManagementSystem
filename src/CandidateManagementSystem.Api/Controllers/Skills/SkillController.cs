using CandidateManagementSystem.Application.Skills.AddSkill;
using CandidateManagementSystem.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CandidateManagementSystem.Api.Controllers.Skills;

[ApiController]
[Route("api/skills")]
public class SkillController : ControllerBase
{
    private readonly ISender _sender;
    
    public SkillController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddSkill([FromBody] AddSkillRequest request, CancellationToken cancellationToken)
    {
        AddSkillCommand command = new(request.Name);

        Result<Guid> result = await _sender.Send(command, cancellationToken);
        
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        
        return Ok(result.Value);
    }
}