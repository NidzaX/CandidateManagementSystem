using System.Globalization;
using CandidateManagementSystem.Application.JobCandidates.AddJobCandidate;
using CandidateManagementSystem.Domain.Abstractions;
using CandidateManagementSystem.Domain.JobCandidates;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CandidateManagementSystem.Api.Controllers.JobCandidates;

[ApiController]
[Route("api/job_candidates")]
public class JobCandidateController : ControllerBase
{
    private readonly ISender _sender;
    
    public JobCandidateController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddJobCandidate(
        [FromBody] AddJobCandidateRequest request,
        CancellationToken cancellationToken)
    {
        AddJobCandidateCommand command = new(
            request.FirstName,
            request.LastName,
            DateTime.Parse(
                request.Birth,
                null,
                DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal),
            request.ContactNumber,
            request.Email);
        
        Result<Guid> result = await _sender.Send(command, cancellationToken);
        
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        
        return Ok(result.Value);
    }
}