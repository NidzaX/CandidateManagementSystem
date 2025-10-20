using System.Globalization;
using CandidateManagementSystem.Application.JobCandidates.AddJobCandidate;
using CandidateManagementSystem.Application.JobCandidates.AddSkillToCandidate;
using CandidateManagementSystem.Application.JobCandidates.RemoveJobCandidate;
using CandidateManagementSystem.Application.JobCandidates.RemoveSkillFromCandidate;
using CandidateManagementSystem.Application.JobCandidates.SearchJobCandidates;
using CandidateManagementSystem.Application.JobCandidates.UpdateCandidate;
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

    [HttpPost("assign-skill")]
    public async Task<IActionResult> AddSkillToJobCandidate(
        [FromBody] AddSkillToCandidateRequest request,
        CancellationToken cancellationToken)
    {
        AddSkillToCandidateCommand command = new(request.JobCandidateId, request.SkillId);

        Result<Guid> result = await _sender.Send(command, cancellationToken);
        
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
    
        return Ok(result.Value);
    }

    [HttpDelete("{candidateId:guid}/skills/{skillId:guid}")]
    public async Task<IActionResult> RemoveSkillFromJobCandidate(
        Guid candidateId,
        Guid skillId,
        CancellationToken cancellationToken)
    {
        RemoveSkillFromCandidateCommand command = new(candidateId, skillId);

        Result<Guid> result = await _sender.Send(command, cancellationToken);
        
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
    
        return Ok(result.Value);
    }
    
    [HttpDelete("{candidateId:guid}")]
    public async Task<IActionResult> RemoveJobCandidate(
        Guid candidateId,
        CancellationToken cancellationToken)
    {
        RemoveJobCandidateCommand command = new(candidateId);

        Result<Guid> result = await _sender.Send(command, cancellationToken);
    
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpPut("{candidateId:guid}")]
    public async Task<IActionResult> UpdateJobCandidate(
        Guid candidateId,
        [FromBody] UpdateCandidateRequest request,
        CancellationToken cancellationToken)
    {
        UpdateCandidateCommand command = new(
            candidateId,
            request.FirstName,
            request.LastName,
            DateTime.Parse(request.Birth, null, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal),
            request.ContactNumber,
            request.Email,
            request.SkillIds);

        Result<Guid> result = await _sender.Send(command, cancellationToken);
        
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchCandidates(
        [FromQuery] string? name,
        [FromQuery] List<string> skills,
        CancellationToken cancellationToken)
    {
        SearchJobCandidatesQuery query = new(name, skills);
        Result<List<JobCandidate>> result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        
        List<CandidateSearchResponse> response = result.Value.Select(candidate => new CandidateSearchResponse(
            candidate.Id,
            candidate.FirstName.Value,
            candidate.LastName.Value,
            candidate.Birth,
            candidate.ContactNumber.Value,
            candidate.Email.Value,
            candidate.Skills.Select(s => new SkillResponse(s.Id, s.Name.Value)).ToList()
        )).ToList();

        return Ok(response);
    }
}